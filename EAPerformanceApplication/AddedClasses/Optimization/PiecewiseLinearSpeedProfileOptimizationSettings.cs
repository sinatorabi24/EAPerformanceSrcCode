using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EAPerformanceApplication.AddedClasses.Optimization
{
    [DataContract]
    public class PiecewiseLinearSpeedProfileOptimizationSettings
    {
        [DataMember]
        public int PopulationSize { get; set; }
        [DataMember]
        public double TournamentSelectionParameter { get; set; }
        [DataMember]
        public int TournamentSize { get; set; }
        [DataMember]
        public double CrossoverProbability { get; set; }
        [DataMember]
        public double RelativeMutationProbability { get; set; }
        [DataMember]
        public double CreepMutationRate { get; set; }
        [DataMember]
        public double MaximumSpeedValue { get; set; }
        [DataMember]
        public double MinimumSpeedValue { get; set; }
        [DataMember]
        public double SpeedIncrement { get; set; }
        [DataMember]
        public int NumberOfRoadSegments { get; set; }
        [DataMember]
        public double DesiredAverageSpeed { get; set; }
        [DataMember]
        public double OptimizationTime { get; set; }
        [DataMember]
        public int NumberOfGenerations { get; set; }
                
        public void SetDefault()
        {
            PopulationSize = 20; 
            TournamentSelectionParameter = 0.9;
            TournamentSize = 5;
            CrossoverProbability = 1.0;
            RelativeMutationProbability = 2.5;
            CreepMutationRate = 0.5;
            MaximumSpeedValue = 25;
            MinimumSpeedValue = 60 / 3.6;
            SpeedIncrement = 4 / 3.6;
            NumberOfRoadSegments = 7;
            DesiredAverageSpeed = 80 / 3.6;
            OptimizationTime = 100000000;
            NumberOfGenerations = 50;            
        }
    }
}
