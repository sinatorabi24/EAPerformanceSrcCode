using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutonomousVehicleLibrary.Vehicles;
using AutonomousVehicleLibrary.Simulations;

namespace EAPerformanceApplication.AddedClasses.SimulationClasses
{
    public class SingleVehicleSimulation : Simulation
    {
        private const double DISTANCE_TO_END_POINT = 1;

        protected override void CheckTerminationCriteria()
        {
            if (Math.Abs(VehicleList[0].PartList[0].Position[0] - metricPath.PathPointList.Last().Point.CoordinateList[0]) < DISTANCE_TO_END_POINT)
            {
                this.running = false;
            }
        }

        protected override void UpdateLog()
        {
            foreach (Vehicle vehicle in vehicleList)
            {
                log.Update(currentDateTime, vehicle);
            }
        }
    }
}
