using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MathematicsLibrary;

namespace EAPerformanceApplication.AddedClasses
{
    [DataContract]
    public class LinearBezierSpline
    {
        [DataMember]
        public Point P0 { get; set; }
        [DataMember]
        public Point P1 { get; set; }

        /// This is the default constructor, which simply
        /// generates the two control points P0 and P1 
        /// (of type Point).
        public LinearBezierSpline(int numberOfDimensions)
        {
            P0 = new Point(numberOfDimensions);
            P1 = new Point(numberOfDimensions);
        }

        /// This method returns a Point object containing the x and y
        /// coordinates at a given value of u (which should be in the range [0,1]).
        public Point GetPoint(double u)
        {
            List<double> coordinateList = new List<double>();
            int numberOfDimensions = P0.GetNumberOfDimensions();
            for (int ii = 0; ii < numberOfDimensions; ii++)
            {
                double coordinate = P0.CoordinateList[ii] * (1 - u) + P1.CoordinateList[ii] * u;
                coordinateList.Add(coordinate);
            }
            Point point = new Point(coordinateList);
            return point;
        }

        public string AsString()
        {
            string bezierSplineAsString = P0.CoordinateList[0].ToString() + "; " + P0.CoordinateList[1].ToString() + "; " +
                        P1.CoordinateList[0].ToString() + "; " + P1.CoordinateList[1].ToString();
            return bezierSplineAsString;
        }
    }
}
