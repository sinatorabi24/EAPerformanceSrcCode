using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutonomousVehicleLibrary.Maps;
using AutonomousVehicleLibrary.Navigation;

namespace EAPerformanceApplication.AddedClasses
{
    public class PiecewiseLinearSpeedProfile
    {
        private List<SegmentLinearSpeedProfile> segmentLinearSpeedProfileList;
        private int numberOfLines;
        private List<List<double>> connectionPointsList;

        public PiecewiseLinearSpeedProfile(MetricPath path, int numberOfLines)
        {
            this.numberOfLines = numberOfLines;
            connectionPointsList = new List<List<double>>();

            int numberOfPathPoints = path.DistanceList.Count;
            double roadLength = path.GetLength();
            double distanceCoveredInEachSection = roadLength / numberOfLines;
            double distanceCoveredSoFar = 0;

            for (int ii = 0; ii < numberOfLines; ii++)
            {
                distanceCoveredSoFar += distanceCoveredInEachSection;
                List<double> linearSectionInfoList = new List<double>();
                linearSectionInfoList.Add(ii);
                linearSectionInfoList.Add(distanceCoveredSoFar);
                connectionPointsList.Add(linearSectionInfoList);
            }
        }

        public double ComputeAverageSpeed(MetricPath path)
        {
            double averageSpeed = 0;

            double roadLength = path.GetLength();
            double roadLengthCoveredInEachSegment = roadLength / numberOfLines;
            double totalTravellingTime = 0;

            for (int ii = 0; ii < numberOfLines; ii++)
            {
                double entrySpeed = this.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[ii].P0.CoordinateList[0];
                double exitSpeed = this.SegmentLinearSpeedProfileList[0].LinearBezierSplineList[ii].P1.CoordinateList[0];
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

            averageSpeed = roadLength / totalTravellingTime;

            return averageSpeed;
        }

        public void Generate(MetricPath path, List<double> speedList)
        {
            segmentLinearSpeedProfileList = new List<SegmentLinearSpeedProfile>();
            foreach (MapSegment mapSegment in path.MapSegmentList)
            {
                SegmentLinearSpeedProfile segmentLinearSpeedProfile = new SegmentLinearSpeedProfile();
                segmentLinearSpeedProfile.Generate(speedList, numberOfLines);
                segmentLinearSpeedProfileList.Add(segmentLinearSpeedProfile);
            }
        }

        public void GenerateConstant(MetricPath path, double speed)
        {
            segmentLinearSpeedProfileList = new List<SegmentLinearSpeedProfile>();
            foreach (MapSegment mapSegment in path.MapSegmentList)
            {
                SegmentLinearSpeedProfile segmentLinearSpeedProfile = new SegmentLinearSpeedProfile();
                segmentLinearSpeedProfile.GenerateConstant(mapSegment, speed, numberOfLines);
                segmentLinearSpeedProfileList.Add(segmentLinearSpeedProfile);
            }
        }

        public List<SegmentLinearSpeedProfile> SegmentLinearSpeedProfileList
        {
            get { return segmentLinearSpeedProfileList; }
            set { segmentLinearSpeedProfileList = value; }
        }

        public List<List<double>> ConnectionPointsList
        {
            get { return connectionPointsList; }
            set { connectionPointsList = value; }
        }
    }
}
