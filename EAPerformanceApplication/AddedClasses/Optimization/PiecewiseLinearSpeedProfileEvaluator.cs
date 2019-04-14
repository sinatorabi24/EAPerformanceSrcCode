using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutonomousVehicleLibrary;
using AutonomousVehicleLibrary.Controllers;
using AutonomousVehicleLibrary.Engines;
using AutonomousVehicleLibrary.Drivers;
using AutonomousVehicleLibrary.Logging;
using AutonomousVehicleLibrary.Vehicles.Gearboxes;
using AutonomousVehicleLibrary.Maps;
using AutonomousVehicleLibrary.Navigation;
using AutonomousVehicleLibrary.Vehicles;
using AutonomousVehicleLibrary.Missions;
using AutonomousVehicleLibrary.Sensors;
using AutonomousVehicleLibrary.Simulations;
using AutonomousVehicleLibrary.Vehicles.States;
using MathematicsLibrary.Vectors;
using StochasticOptimizationLibrary;
using StochasticOptimizationLibrary.Parameters;
using EAPerformanceApplication.AddedClasses.SimulationClasses;

namespace EAPerformanceApplication.AddedClasses.Optimization
{
    public class PiecewiseLinearSpeedProfileEvaluator
    {
        #region Fields
        private PiecewiseLinearSpeedProfile speedProfile;
        private MetricPath metricPath;
        private Vehicle vehicle;
        private PiecewiseLinearSpeedProfileDriver driver;
        private MetricMap metricMap;
        private Mission mission;
        private OptimizableStructure speedProfileIndividual;
        private SingleVehicleSimulation singleRunSimulation;
        private Log loggedData;
        private double averageSpeed;
        private double topSpeed;
        private double minSpeed;
        private double fuelConsumption;

        private double desiredAverageSpeed;
        private double maxAllowedSpeed;
        private double minAllowedSpeed;
        private List<double> possibleSpeedList;
        #endregion

        #region Private methods
        private RigidTruck GenerateVehicle(double mass)
        {
            RigidTruck vehicle = new RigidTruck();
            vehicle.Name = "Vehicle";
            vehicle.PartList[0].Length = 10.0;
            vehicle.PartList[0].Mass = mass;
            vehicle.PartList[0].FrontalArea = 10.26;
            vehicle.PartList[0].Height = 3.504;
            vehicle.PartList[0].RollingResistanceCoefficient = 0.0047;
            vehicle.PartList[0].WheelRadius = 0.491;
            vehicle.PartList[0].WheelInertia = 171;
            vehicle.PartList[0].AirDragCoefficient = 0.65;
            vehicle.PartList[0].AirDensity = 1.34;
            vehicle.PartList[0].FinalDriveEfficiency = 0.97;
            vehicle.PartList[0].FinalDriveRatio = 3.1; // 20170203, ST modified the value so it is now compatible with the experiment truck.
            vehicle.PartList[0].LinearAcceleration = 0;
            vehicle.CurrentGear = 1;

            // Engine
            vehicle.Engine = new TruckEngine();
            vehicle.Engine.EngineInertia = 3.8;

            //Gearbox
            // NOTE: when changing the gear ratios and gear efficiency list (in case a new truck 
            // is used), make sure that the fictitious gears are added as well (usually four 
            // additional gears). ST, 20170202.
            vehicle.Gearbox = new TruckGearbox();
            vehicle.Gearbox.GearRatios = new List<double> { 11.7293, 9.2105, 7.0941, 5.5707, 4.3478, 3.4142, 2.6977, 2.1184, 1.6316, 1.2813, 1.0000, 0.7853 };
            vehicle.Gearbox.NumberOfGears = vehicle.Gearbox.GearRatios.Count;
            vehicle.Gearbox.GearRatios.AddRange(new List<double> { 0.7853, 0.7853, 0.7853, 0.7853 });
            vehicle.Gearbox.GearEfficiencyList = new List<double> { 0.9700, 0.9720, 0.9720, 0.9740, 0.9860, 0.9730, 0.9800, 0.9820, 0.9820, 0.9840, 0.9960, 0.9830, 0.9830, 0.9830, 0.9830, 0.9830 };
            vehicle.Gearbox.InitializeGearbox();

            // Define and add the vehicle controller
            double proportionalGain = 10000;
            double integralGain = 200;
            double derivativeGain = 100;
            PIDController linearAccelerationController = new PIDController(proportionalGain, integralGain, derivativeGain);
            linearAccelerationController.SetVehicle(vehicle);
            ((DynamicVehicle)vehicle).LinearAccelerationController = linearAccelerationController;

            // Generate and add sensors:
            GPS gps = new GPS();
            gps.Name = "GPS";
            gps.ReadingInterval = 0.01;
            gps.StorageDuration = 0.05;
            vehicle.AddSensor(gps);
            Speedometer speedometer = new Speedometer();
            speedometer.Name = "Speedometer";
            speedometer.ReadingInterval = 0.01;
            speedometer.StorageDuration = 0.05;
            vehicle.AddSensor(speedometer);
            Accelerometer accelerometer = new Accelerometer();
            accelerometer.Name = "Accelerometer";
            accelerometer.ReadingInterval = 0.01;
            accelerometer.StorageDuration = 0.05;
            vehicle.AddSensor(accelerometer);
            FuelConsumptionSensor fuelConsumptionSensor = new FuelConsumptionSensor();
            fuelConsumptionSensor.Name = "FuelConsumptionSensor";
            fuelConsumptionSensor.ReadingInterval = 0.01;
            fuelConsumptionSensor.StorageDuration = 0.05;
            vehicle.AddSensor(fuelConsumptionSensor);
            TransmissionCurrentGearSensor currentGearSensor = new TransmissionCurrentGearSensor();
            currentGearSensor.Name = "TransmissionCurrentGearSensor";
            currentGearSensor.ReadingInterval = 0.01;
            currentGearSensor.StorageDuration = 0.05;
            vehicle.AddSensor(currentGearSensor);
            EngineTorqueSensor engineTorqueSensor = new EngineTorqueSensor();
            engineTorqueSensor.Name = "EngineTorqueSensor";
            engineTorqueSensor.ReadingInterval = 0.01;
            engineTorqueSensor.StorageDuration = 0.05;
            vehicle.AddSensor(engineTorqueSensor);
            EngineSpeedSensor engineSpeedSensor = new EngineSpeedSensor();
            engineSpeedSensor.Name = "EngineSpeedSensor";
            engineSpeedSensor.ReadingInterval = 0.01;
            engineSpeedSensor.StorageDuration = 0.05;
            vehicle.AddSensor(engineSpeedSensor);
            FuelRateSensor currentFuelConsumptionSensor = new FuelRateSensor();
            currentFuelConsumptionSensor.Name = "FuelRateSensor";
            currentFuelConsumptionSensor.ReadingInterval = 0.01;
            currentFuelConsumptionSensor.StorageDuration = 0.05;
            vehicle.AddSensor(currentFuelConsumptionSensor);

            // Define and add the vehicle state (estimate)
            VehicleState vehicleState = new VehicleState();
            VehicleStateItem xItem = new VehicleStateItem();
            xItem.SensorName = gps.Name;
            xItem.SensorItemName = "X";
            xItem.Name = "X";
            vehicleState.ItemList.Add(xItem);
            VehicleStateItem zItem = new VehicleStateItem();
            zItem.Name = "Z";
            zItem.SensorName = gps.Name;
            zItem.SensorItemName = "Z";
            vehicleState.ItemList.Add(zItem);
            vehicle.VehicleState = vehicleState;
            VehicleStateItem headingItem = new VehicleStateItem();
            headingItem.Name = "Heading";
            headingItem.SensorName = gps.Name;
            headingItem.SensorItemName = "Heading";
            vehicleState.ItemList.Add(headingItem);
            VehicleStateItem speedItem = new VehicleStateItem();
            speedItem.Name = "Speed";
            speedItem.SensorName = speedometer.Name;
            speedItem.SensorItemName = "Speed";
            vehicleState.ItemList.Add(speedItem);
            VehicleStateItem accelerationItem = new VehicleStateItem();
            accelerationItem.Name = "Acceleration";
            accelerationItem.SensorName = accelerometer.Name;
            accelerationItem.SensorItemName = "Acceleration";
            vehicleState.ItemList.Add(accelerationItem);
            VehicleStateItem fuelConsumptionItem = new VehicleStateItem();
            fuelConsumptionItem.Name = "FuelConsumption";
            fuelConsumptionItem.SensorName = fuelConsumptionSensor.Name;
            fuelConsumptionItem.SensorItemName = "FuelConsumption";
            vehicleState.ItemList.Add(fuelConsumptionItem);
            VehicleStateItem currentFuelConsumptionItem = new VehicleStateItem();
            currentFuelConsumptionItem.Name = "FuelRate";
            currentFuelConsumptionItem.SensorName = currentFuelConsumptionSensor.Name;
            currentFuelConsumptionItem.SensorItemName = "FuelRateSensor";
            vehicleState.ItemList.Add(currentFuelConsumptionItem);
            VehicleStateItem currentGearItem = new VehicleStateItem();
            currentGearItem.Name = "TransmissionCurrentGear";
            currentGearItem.SensorName = currentGearSensor.Name;
            currentGearItem.SensorItemName = "TransmissionCurrentGearSensor";
            vehicleState.ItemList.Add(currentGearItem);
            VehicleStateItem engineTorque = new VehicleStateItem();
            engineTorque.Name = "EngineTorque";
            engineTorque.SensorName = engineTorqueSensor.Name;
            engineTorque.SensorItemName = "EngineTorqueSensor";
            vehicleState.ItemList.Add(engineTorque);
            VehicleStateItem engineSpeed = new VehicleStateItem();
            engineSpeed.Name = "EngineSpeed";
            engineSpeed.SensorName = engineSpeedSensor.Name;
            engineSpeed.SensorItemName = "EngineSpeedSensor";
            vehicleState.ItemList.Add(engineSpeed);

            return vehicle;
        }

        private PiecewiseLinearSpeedProfileDriver GenerateDriver(Vehicle vehicle)
        {
            PiecewiseLinearSpeedProfileDriver driver = new PiecewiseLinearSpeedProfileDriver();
            driver.Name = "Driver";
            driver.Vehicle = vehicle;
            return driver;
        }

        private void GenerateMission(Driver driver)
        {

            PiecewiseLinearSpeedProfileMission mission = new PiecewiseLinearSpeedProfileMission();
            PiecewiseLinearSpeedProfileMissionItem missionItem = new PiecewiseLinearSpeedProfileMissionItem();
            missionItem.MetricPath = metricPath;
            missionItem.PiecewiseLinearSpeedProfile = speedProfile;
            mission.MissionItemList.Add(missionItem);
            driver.AssignMission(mission);
        }

        private void GenerateInitialPose()
        {
            Vector tangentVector = metricPath.MapSegmentList[0].NaturalCubicSplineList[0].GetTangent(0);
            double slopeAngle = Math.Atan2(tangentVector.ComponentList[2], tangentVector.ComponentList[0]);

            RoadPoint startPoint = metricMap.RoadPointList[0];
            vehicle.SetPose(startPoint.X, startPoint.Y, startPoint.Z, 0, slopeAngle); // MW ToDo: Perhaps set via the metric path instead?

            vehicle.PartList[0].Velocity[0] = speedProfile.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[0].P0.CoordinateList[0] *
                Math.Cos(vehicle.PartList[0].Heading) * Math.Cos(vehicle.PartList[0].Pitch);
            vehicle.PartList[0].Velocity[1] = speedProfile.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[0].P0.CoordinateList[0] *
                Math.Sin(vehicle.PartList[0].Heading) * Math.Cos(vehicle.PartList[0].Pitch);
            vehicle.PartList[0].Velocity[2] = speedProfile.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[0].P0.CoordinateList[0] *
                Math.Sin(vehicle.PartList[0].Pitch);
        }

        private void GenerateMission()
        {
            // Generate and assign mission:
            mission = new PiecewiseLinearSpeedProfileMission();
            PiecewiseLinearSpeedProfileMissionItem missionItem = new PiecewiseLinearSpeedProfileMissionItem();
            missionItem.MetricPath = metricPath;
            ((PiecewiseLinearSpeedProfileMissionItem)missionItem).PiecewiseLinearSpeedProfile = speedProfile;

            mission.MissionItemList.Add(missionItem);
            driver.AssignMission(mission);
            vehicle.ResetTime(DateTime.Now);
        }

        private void GenerateSimulation()
        {
            singleRunSimulation = new SingleVehicleSimulation();
            singleRunSimulation.VehicleList.Add(vehicle);
            singleRunSimulation.DriverList.Add(driver);
            singleRunSimulation.MetricPath = mission.CurrentPath;
            MetricPathLocator vehicleLocator = new MetricPathLocator(vehicle.Name, 0);
            singleRunSimulation.VehicleLocatorList.Add(vehicleLocator);

            singleRunSimulation.RunInRealTime = false;
            singleRunSimulation.LogData = true;

            foreach (VehicleStateItem stateItem in vehicle.VehicleState.ItemList)
            {
                LogItem logItem = new LogItem();
                logItem.Name = stateItem.Name;
                logItem.VehicleStateItemName = stateItem.Name;
                singleRunSimulation.Log.AddLogItem(logItem);
            }

            singleRunSimulation.SetCurrentDateTime(DateTime.Now);
            singleRunSimulation.RunningMode = RunningMode.Synchronous;
        }

        private double ComnputeAverageSpeed(List<double> speedList, int numberOfSegments)
        {
            double averageSpeed = 0;
            double pathLength = metricPath.GetLength();

            double roadLengthCoveredInEachSegment = pathLength / numberOfSegments;
            double totalTravellingTime = 0;

            for (int ii = 0; ii < numberOfSegments; ii++)
            {
                double entrySpeed = speedList[ii];
                double exitSpeed = speedList[ii + 1];
                double speedDifference = exitSpeed - entrySpeed;

                if (Math.Abs(speedDifference) < double.Epsilon)
                {
                    totalTravellingTime += roadLengthCoveredInEachSegment / entrySpeed;
                }
                else
                {
                    totalTravellingTime += Math.Log(exitSpeed / entrySpeed) * roadLengthCoveredInEachSegment / speedDifference;
                }
            }

            averageSpeed = pathLength / totalTravellingTime;

            return averageSpeed;
        }
#endregion

        #region Public methods
        public void Initialize()
        {
            vehicle = GenerateVehicle(30000); //Mass in kg
            driver = GenerateDriver(vehicle);
            GenerateInitialPose();
            GenerateMission();
            GenerateSimulation();
        }

        // Used when (re-)evaluating a given speed profile set, rather than obtaining
        // it via decoding (as in a GA).
        public void AssignSpeedProfile(PiecewiseLinearSpeedProfile speedProfile)
        {
            this.speedProfile = speedProfile;
        }

        public void AssignPossibleSpeedList(List<double> possibleSpeedList)
        {
            this.possibleSpeedList = possibleSpeedList;
        }

        public List<double> DecodeSpeedProfile(OptimizableStructure individual)
        {
            //this.speedProfileIndividual = individual;

            int numberOfLineSegments = individual.ParameterList.Count - 1;

            List<double> speedSequence = new List<double>();

            for (int i = 0; i < numberOfLineSegments + 1; i++)
            {
                int speedIndex = ((IntParameter)individual.ParameterList[i]).ParameterValue;
                speedSequence.Add(possibleSpeedList[speedIndex]);
            }
            speedProfile = new PiecewiseLinearSpeedProfile(metricPath, numberOfLineSegments);
            speedProfile.Generate(metricPath, speedSequence);

            return speedSequence;
        }

        public void AssignMetricPath(MetricPath metricPath)
        {
            this.metricPath = metricPath;
        }

        public void AssignMetricMap(MetricMap metricMap)
        {
            this.metricMap = metricMap;
        }

        public double Evaluate()
        {
            double fuelConsumption = 0;
            Initialize();

            singleRunSimulation.Start();

            loggedData = singleRunSimulation.Log;

            fuelConsumption = loggedData.LogItemList.Find(i => i.Name == "FuelConsumption").ValueList.Last();

            List<double> vehicleSpeedList = new List<double>();
            vehicleSpeedList = loggedData.LogItemList.Find(i => i.Name == "Speed").ValueList;
            averageSpeed = vehicleSpeedList.Average();
            topSpeed = vehicleSpeedList.Max();

            return fuelConsumption;
        }

        public double EvaluateGA(OptimizableStructure individual)
        {
            List<double> speedSequence = DecodeSpeedProfile(individual);
            int numberOfLineSegments = individual.ParameterList.Count - 1;
            averageSpeed = ComnputeAverageSpeed(speedSequence, numberOfLineSegments);

            Initialize();
            
            singleRunSimulation.Start();

            loggedData = singleRunSimulation.Log;

            fuelConsumption = loggedData.LogItemList.Find(i => i.Name == "FuelConsumption").ValueList.Last();

            List<double> vehicleSpeedList = new List<double>();
            vehicleSpeedList = loggedData.LogItemList.Find(i => i.Name == "Speed").ValueList;
            topSpeed = vehicleSpeedList.Max();
            vehicleSpeedList.RemoveAt(0);
            minSpeed = vehicleSpeedList.Min();
            vehicleSpeedList.Insert(0, 0);
                
            if (Math.Abs(averageSpeed - desiredAverageSpeed) < 1 / 3.6)
            {
                return (1 / fuelConsumption);
            }
            else
            {
                return 0;
            }            
        }
        #endregion

        #region Properties
        public double AverageSpeed
        {
            get { return averageSpeed; }
        }

        public double MinimumInstantanouesSpeed
        {
            get { return minSpeed; }
        }

        public double MaximumInstantanouesSpeed
        {
            get { return topSpeed; }
        }

        public Log LoggedData
        {
            get { return loggedData; }
        }

        public double FuelConsumption
        {
            get { return fuelConsumption; }
        }

        public double DesiredAverageSpeed
        {
            get { return desiredAverageSpeed; }
            set { desiredAverageSpeed = value; }
        }

        public double MinimumAllowedSpeed
        {
            get { return minAllowedSpeed; }
            set { minAllowedSpeed = value; }
        }

        public double MaximumAllowedSpeed
        {
            get { return maxAllowedSpeed; }
            set { maxAllowedSpeed = value; }
        }

        public List<double> PossibleSpeedList
        {
            get { return possibleSpeedList; }
        }
        #endregion
    }
}
