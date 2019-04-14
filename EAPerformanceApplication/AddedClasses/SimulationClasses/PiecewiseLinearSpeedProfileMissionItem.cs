using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using AutonomousVehicleLibrary.Maps;
using AutonomousVehicleLibrary.Navigation;
using AutonomousVehicleLibrary.Missions;

namespace EAPerformanceApplication.AddedClasses
{
    [DataContract]
    public class PiecewiseLinearSpeedProfileMissionItem : MissionItem
    {
        private PiecewiseLinearSpeedProfile piecewiseLinearSpeedProfile;

        [DataMember]
        public PiecewiseLinearSpeedProfile PiecewiseLinearSpeedProfile
        {
            get { return piecewiseLinearSpeedProfile; }
            set { piecewiseLinearSpeedProfile = value; }
        }
    }
}
