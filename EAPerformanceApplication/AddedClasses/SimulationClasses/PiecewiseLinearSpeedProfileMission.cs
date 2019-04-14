using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AutonomousVehicleLibrary.Missions;

namespace EAPerformanceApplication.AddedClasses
{
    [DataContract]
    public class PiecewiseLinearSpeedProfileMission : Mission
    {
        private PiecewiseLinearSpeedProfile piecewiseLinearSpeedProfile = null;

        [DataMember]
        public PiecewiseLinearSpeedProfile PiecewiseLinearSpeedProfile
        {
            get { return piecewiseLinearSpeedProfile; }
            set { piecewiseLinearSpeedProfile = value; }
        }
    }
}
