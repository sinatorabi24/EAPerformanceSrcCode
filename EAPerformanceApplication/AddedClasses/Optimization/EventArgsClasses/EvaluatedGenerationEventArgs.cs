using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPerformanceApplication.AddedClasses.Optimization.EventArgsClasses
{
    public class EvaluatedGenerationEventArgs : EventArgs
    {
        private double bestFitness;
        private double averageFitness;
        private int generationIndex;
        private List<int> bestIndividual;
        private long bestIndividualIndex;
        private int numberOfEvaluatedIndividuals;

        public EvaluatedGenerationEventArgs(double bestFitness, double averageFitness, int generationIndex, 
            int numberOfEvaluatedIndividuals, List<int> bestIndividual, long bestIndividualIndex)
        {
            this.bestFitness = bestFitness;
            this.averageFitness = averageFitness;
            this.bestIndividual = bestIndividual;
            this.bestIndividualIndex = bestIndividualIndex;
            this.generationIndex = generationIndex;
            this.numberOfEvaluatedIndividuals = numberOfEvaluatedIndividuals;            
        }

        public double BestFitness
        {
            get { return bestFitness; }
        }

        public double AverageFitness
        {
            get { return averageFitness; }
        }

        public int GenerationIndex
        {
            get { return generationIndex; }
        }

        public List<int> BestIndividual
        {
            get { return bestIndividual; }
        }

        public long BestIndividualIndex
        {
            get { return bestIndividualIndex; }
        }

        public int NumberOfEvaluatedIndividuals
        {
            get { return numberOfEvaluatedIndividuals; }
        }
    }
}
