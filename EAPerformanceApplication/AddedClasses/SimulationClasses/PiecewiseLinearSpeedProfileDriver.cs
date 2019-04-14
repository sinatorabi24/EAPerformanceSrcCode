using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutonomousVehicleLibrary.Drivers;
using AutonomousVehicleLibrary.Missions;
using AutonomousVehicleLibrary.Navigation;
using MathematicsLibrary;

namespace EAPerformanceApplication.AddedClasses
{
    public class PiecewiseLinearSpeedProfileDriver : Driver
    {
        private const double GPS_PRECISION = 1.0; // MW ToDo: This constant (if used at all!) should definitely not be here!
        private const double MAXIMUM_SPEED_CONSTANT = 100; // MW ToDo: Ugly - this constant is defined in two places! Change!

        private double u;
        private int currentSplineIndex;

        private MetricPathLocator estimatedLocation;
        private DateTime previousEstimateDateTime;
        private Boolean firstEstimateMade = false;
        private int previousPathPointIndex;

        public override void AssignMission(Mission mission)
        {
            base.AssignMission(mission);

            // 20160411 MW (A bit ugly, but OK for now. Later on, we will remove
            // the CurrentPath and SpeedProfile in Mission, and instead take them
            // directly from mission.MissionItemList[currentMissionItemIndex] ... etc.
            mission.CurrentPath = mission.MissionItemList[0].MetricPath;
            ((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile = ((PiecewiseLinearSpeedProfileMissionItem)mission.MissionItemList[0]).PiecewiseLinearSpeedProfile;
            // END 20160411

            Point point = mission.CurrentPath.MapSegmentList[0].NaturalCubicSplineList[0].GetPoint(0);
            previousPathPointIndex = 0; // Also the current path point, when starting!
            estimatedLocation = new MetricPathLocator(this.vehicle.Name, previousPathPointIndex);
            firstEstimateMade = false;
        }

        private void EstimatePathLocation(DateTime dateTime)
        {
            double maximumBackwardRange = 0;
            double maximumForwardRange = 0;
            if (!firstEstimateMade)
            {
                maximumBackwardRange = GPS_PRECISION;
                maximumForwardRange = GPS_PRECISION;

            }
            else
            {
                double timeSinceLastEstimate = elapsedTimeSinceLastAction;
                maximumBackwardRange = GPS_PRECISION; // In case the vehicle is standing still, for example. (NOTE: (Actual) backward motion NOT considered here!)
                maximumForwardRange = GPS_PRECISION + timeSinceLastEstimate * MAXIMUM_SPEED_CONSTANT;
            }
            Point point = new Point(3);

            // Here is the difference compared with the exact location (in the Simulation class):
            double x = this.vehicle.VehicleState.GetItem("X").StateValue;
            double z = this.vehicle.VehicleState.GetItem("Z").StateValue;
            point.CoordinateList[0] = x;
            point.CoordinateList[1] = 0; // A bit ugly, but OK for this special case.
            point.CoordinateList[2] = z;

            //MetricPath metricPath = this.mission.CurrentPath;
            //int pathPointBeforeIndex = metricPath.GetPathPointIndex(point, estimatedLocation.PathPointIndex, maximumBackwardRange, maximumForwardRange);
            //estimatedLocation.PathPointIndex = pathPointBeforeIndex;
            //MetricPathPoint pathPointBefore = metricPath.PathPointList[pathPointBeforeIndex];
            //double uBefore = metricPath.PathPointList[pathPointBeforeIndex].U;
            //// Handle the transition between splines:
            //MetricPathPoint pathPointAfter = metricPath.PathPointList[pathPointBeforeIndex + 1];
            //double uAfter;
            //if (pathPointAfter.SplineIndex != pathPointBefore.SplineIndex) { uAfter = 1; }
            //else { uAfter = metricPath.PathPointList[pathPointBeforeIndex + 1].U; }

            //NaturalCubicSpline currentSpline = metricPath.MapSegmentList[pathPointBefore.SegmentIndex].NaturalCubicSplineList[pathPointBefore.SplineIndex];
            //// double u = currentSpline.GetUAtPoint(point, uBefore, uAfter, NUMBER_OF_U_STEPS);   // MW ToDo: parameterize (remove constant?)
            //// Sina ToDo: store u and the spline index so that it is available elsewhere in the driver.
            ////            The driver can then obtain the speed (roughly) as SpeedProfile.SegmentSpeedProfileList[0].NaturalCubicSplineList[splineIndex].GetPoint(u)
            ////            The point will contain one dimension (= the speed) in this case.

            // Added on 20171124 (for test of optimality), ST.
            currentSplineIndex = GetSplineIndex(previousPathPointIndex, x);
            if (currentSplineIndex == 1) { double aaa = 0; }
            u = GetUAtPoint(currentSplineIndex, x);

            //// Maybe not needed?
            //Vector tangentVector = currentSpline.GetTangent(u);
            //double slopeAngle = Math.Atan2(tangentVector.ComponentList[2], tangentVector.ComponentList[0]);

            if (!firstEstimateMade) { firstEstimateMade = true; }
            previousEstimateDateTime = dateTime;
        }

        public override void GenerateAction(DateTime dateTime)
        {
            base.GenerateAction(dateTime); // MW Added 20160205
            EstimatePathLocation(dateTime);
            this.desiredSpeed = ((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[currentSplineIndex].GetPoint(u).CoordinateList[0];
        }

        private int GetSplineIndex(int startPointIndex, double distance)
        {
            int index = startPointIndex;
            while (index < ((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile.ConnectionPointsList.Count)
            {
                if (((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile.ConnectionPointsList[index][1] >= distance) { return (index); }
                index++;
            }
            return ((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile.ConnectionPointsList.Count - 1; // Meaning that the end of the path has been reached (Should this method return distanceList.Count? MW ToDo: Check!)
        }

        private double GetUAtPoint(int splineIndex, double distance)
        {
            double currentSplineEntryDistance = 0;
            double currentSplineExitDistance = ((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile.ConnectionPointsList[splineIndex][1];

            if (splineIndex > 0) { currentSplineEntryDistance = ((PiecewiseLinearSpeedProfileMission)mission).PiecewiseLinearSpeedProfile.ConnectionPointsList[splineIndex - 1][1]; }

            double u = (distance - currentSplineEntryDistance) / (currentSplineExitDistance - currentSplineEntryDistance);

            return u;
        }
    }
}
