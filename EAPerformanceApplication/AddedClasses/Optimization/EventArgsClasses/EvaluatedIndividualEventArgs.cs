using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StochasticOptimizationLibrary;

namespace EAPerformanceApplication.AddedClasses.Optimization.EventArgsClasses
{
    public class EvaluatedIndividualEventArgs : EventArgs
    {
        private double fitness;
        private double averageSpeed;
        private long speedProfileIndex;
        private OptimizableStructure individual;
        private int individualIndexInGeneration;
        private int generationIndex;
        private bool isIndividualFeasible;
        private List<int> averageIndividualSpeedIndex;

        public EvaluatedIndividualEventArgs(List<int> averageIndividualSpeedIndex, long speedProfileIndex,
            double averageSpeed, double fitness, OptimizableStructure individual,
            int individualIndexInGeneration, int generationIndex, bool isIndividualFeasible)
        {
            this.averageIndividualSpeedIndex = averageIndividualSpeedIndex;
            this.fitness = fitness;
            this.averageSpeed = averageSpeed;
            this.speedProfileIndex = speedProfileIndex;
            this.individual = individual;
            this.individualIndexInGeneration = individualIndexInGeneration;
            this.generationIndex = generationIndex;
            this.isIndividualFeasible = isIndividualFeasible;
        }

        public double Fitness
        {
            get { return fitness; }
        }

        public List<int> AverageIndividualSpeedIndex
        {
            get { return averageIndividualSpeedIndex; }
        }

        public double AverageSpeed
        {
            get { return averageSpeed; }
        }

        public long SpeedProfileIndex
        {
            get { return speedProfileIndex; }
        }

        public OptimizableStructure Individual
        {
            get { return individual; }
        }

        public int IndividualIndexInGeneration
        {
            get { return individualIndexInGeneration; }
        }

        public int GenerationIndex
        {
            get { return generationIndex; }
        }

        public bool IsIndividualFeasible
        {
            get { return isIndividualFeasible; }
        }
    }
}
