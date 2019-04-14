using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutonomousVehicleLibrary.Maps;
using MathematicsLibrary.Interpolation;

namespace EAPerformanceApplication.AddedClasses
{
    public class SegmentLinearSpeedProfile
    {
        private List<LinearBezierSpline> linearBezierSplineList;

        public void GenerateConstant(MapSegment mapSegment, double speed, int numberOfLines)
        {
            this.linearBezierSplineList = new List<LinearBezierSpline>();
            for (int ii = 0; ii < numberOfLines; ii++)
            {
                LinearBezierSpline speedProfileLinearBezierSpline = new LinearBezierSpline(1); // 1 = number of dimensions.
                speedProfileLinearBezierSpline.P0.CoordinateList[0] = speed;
                speedProfileLinearBezierSpline.P1.CoordinateList[0] = speed;
                this.linearBezierSplineList.Add(speedProfileLinearBezierSpline);
            }
        }

        public void Generate(List<double> speedList, int numberOfLines)
        {
            this.linearBezierSplineList = new List<LinearBezierSpline>();

            for (int ii = 0; ii < numberOfLines; ii++)
            {
                LinearBezierSpline speedProfileLinearBezierSpline = new LinearBezierSpline(1); // 1 = number of dimensions.
                speedProfileLinearBezierSpline.P0.CoordinateList[0] = speedList[ii];
                speedProfileLinearBezierSpline.P1.CoordinateList[0] = speedList[ii + 1];
                this.linearBezierSplineList.Add(speedProfileLinearBezierSpline);
            }
        }

        public List<LinearBezierSpline> LinearBezierSplineList
        {
            get { return linearBezierSplineList; }
            set { linearBezierSplineList = value; }
        }
    }
}
