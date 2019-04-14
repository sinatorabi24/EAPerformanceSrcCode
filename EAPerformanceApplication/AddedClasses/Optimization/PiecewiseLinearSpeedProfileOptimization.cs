using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutonomousVehicleLibrary;
using AutonomousVehicleLibrary.Maps;
using AutonomousVehicleLibrary.Navigation;
using MathematicsLibrary.Interpolation;
using StochasticOptimizationLibrary;
using StochasticOptimizationLibrary.Parameters;
using EAPerformanceApplication.AddedClasses.Optimization;
using EAPerformanceApplication.AddedClasses.Optimization.EventArgsClasses;

namespace EAPerformanceApplication.AddedClasses.Optimization
{
    public class PiecewiseLinearSpeedProfileOptimization
    {
        private OptimizableStructure optimizedSpeedProfile;
        private List<OptimizableStructure> population;
        private Thread optimizationThread = null;
        private Boolean running = false;
        private Stopwatch stopWatch;
        private double optimizationTime;
        private PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings;
        private PiecewiseLinearSpeedProfileEvaluator speedProfileEvaluator;
        private OptimizationMethod optimizationMethod = OptimizationMethod.GA;
        private MetricMap metricMap;
        private MetricPath metricPath;
        private List<double> possibleSpeedList;
        private Random randomNumberGenerator;
        private int iterationIndex;
        private int numberOfEvaluatedIndividuals;
        private double bestScore;
        private int bestIndividualIndex;
        private List<double> scoreList;
        private double cumulativeScore;

        //For GA
        private int populationSize;
        private double tournamentSelectionParameter;
        private int tournamentSize;
        private double crossoverProbability;
        public double mutationRate;
        private double creepMutationRate;
        private Boolean useElitism = true;
        private int unitLength = 1;
        
        public event EventHandler<EvaluatedIndividualEventArgs> NewBestIndividualFound = null;
        public event EventHandler<EvaluatedGenerationEventArgs> GenerationEvaluated = null;
        public event EventHandler<EvaluatedIndividualEventArgs> IndividualEvaluated = null;
        public event EventHandler Stopped = null;

        private void OnIndividualEvaluated(List<int> individualSpeedIndexList, long speedProfileIndex, double averageSpeed, 
            double fitness, OptimizableStructure individual, int individualIndexInGeneration, 
            int generationIndex, bool isIndividualFeasible)
        {
            if (IndividualEvaluated != null)
            {
                EventHandler<EvaluatedIndividualEventArgs> handler = IndividualEvaluated;
                EvaluatedIndividualEventArgs e = new EvaluatedIndividualEventArgs(individualSpeedIndexList, speedProfileIndex,
                    averageSpeed, fitness, individual, individualIndexInGeneration, generationIndex, isIndividualFeasible);
                handler(this, e);
            }
        }
        
        private void OnStopped()
        {
            if (Stopped != null)
            {
                EventHandler handler = Stopped;
                handler(this, EventArgs.Empty);
            }
        }

        private void OnNewBestIndividualFound(List<int> individualSpeedIndexList, long speedProfileIndex, double averageSpeed,
            double fitness, OptimizableStructure individual, int individualIndexInGeneration, int generationIndex, bool isIndividualFeasible)
        {
            if (NewBestIndividualFound != null)
            {
                EvaluatedIndividualEventArgs e = new EvaluatedIndividualEventArgs(individualSpeedIndexList, speedProfileIndex, averageSpeed, fitness, 
                    individual, individualIndexInGeneration, generationIndex, isIndividualFeasible);
                EventHandler<EvaluatedIndividualEventArgs> handler = NewBestIndividualFound;
                handler(this, e);
            }
        }

        private void OnGenerationEvaluated(double bestFitness, double averageFitness, int generationIndex, int individualIndexInGeneration, 
            int numberOfEvaluatedIndividuals, List<int> bestIndividual, long bestIndividualNumber)
        {
            if (GenerationEvaluated != null)
            {
                EvaluatedGenerationEventArgs e = new EvaluatedGenerationEventArgs(bestFitness, averageFitness, generationIndex,
                    numberOfEvaluatedIndividuals, bestIndividual, bestIndividualNumber);
                EventHandler<EvaluatedGenerationEventArgs> handler = GenerationEvaluated;
                handler(this, e);
            }
        }

        private void SetUpGeneticAlgorithm()
        {
            populationSize = optimizationSettings.PopulationSize;

            tournamentSelectionParameter = optimizationSettings.TournamentSelectionParameter;
            tournamentSize = optimizationSettings.TournamentSize;
            // Note: The mutation rate is set below, after the individuals have been generated
            crossoverProbability = optimizationSettings.CrossoverProbability;
            creepMutationRate = optimizationSettings.CreepMutationRate;
            
            int numberOfLineSegments = optimizationSettings.NumberOfRoadSegments;

            double minimumParameterValue = optimizationSettings.MinimumSpeedValue;
            double maximumParameterValue = optimizationSettings.MaximumSpeedValue;
            double discretizationStep = optimizationSettings.SpeedIncrement;
            double desiredAverageSpeed = optimizationSettings.DesiredAverageSpeed;

            possibleSpeedList = new List<double>();
            double initialSpeed = minimumParameterValue;
            possibleSpeedList.Add(initialSpeed);

            while (initialSpeed < maximumParameterValue)
            {
                initialSpeed += discretizationStep;
                initialSpeed = Math.Min(initialSpeed, maximumParameterValue);
                possibleSpeedList.Add(initialSpeed);
            }

            //returns the index of desired average speed from the possible speed list (generated above). Required for optimizaiton.
            int desiredAverageSpeedIndex = possibleSpeedList.FindIndex(s => s == desiredAverageSpeed);

            OptimizableStructure cruiseControlSpeedProfile = new OptimizableStructure();

            IntParameter p0ParameterFirstIndividual = new IntParameter();
            p0ParameterFirstIndividual.MaximumValue = possibleSpeedList.Count - 1;
            p0ParameterFirstIndividual.MinimumValue = 0;
            p0ParameterFirstIndividual.ParameterValue = desiredAverageSpeedIndex;
            cruiseControlSpeedProfile.ParameterList.Add(p0ParameterFirstIndividual);

            for (int jj = 0; jj < numberOfLineSegments; jj++)
            {
                IntParameter p0Parameter = new IntParameter();
                p0Parameter.MaximumValue = possibleSpeedList.Count - 1;
                p0Parameter.MinimumValue = 0;
                p0Parameter.ParameterValue = desiredAverageSpeedIndex;
                cruiseControlSpeedProfile.ParameterList.Add(p0Parameter);
            }
            population.Add(cruiseControlSpeedProfile.Copy());
            scoreList.Add(double.MinValue);

            for (int ii = 1; ii < populationSize; ii++)
            {
                OptimizableStructure individual = new OptimizableStructure();

                IntParameter p0ParameterFirst = new IntParameter();
                p0ParameterFirst.MaximumValue = possibleSpeedList.Count - 1;
                p0ParameterFirst.MinimumValue = 0;
                p0ParameterFirst.ParameterValue = desiredAverageSpeedIndex;
                individual.ParameterList.Add(p0ParameterFirst);

                for (int jj = 0; jj < numberOfLineSegments; jj++)
                {
                    IntParameter p0Parameter = new IntParameter();
                    p0Parameter.MaximumValue = possibleSpeedList.Count - 1;
                    p0Parameter.MinimumValue = 0;
                    p0Parameter.ParameterValue = desiredAverageSpeedIndex;
                    individual.ParameterList.Add(p0Parameter);
                }

                // Randomize all individuals except the first one:
                OptimizableStructure mutatedIndividual = (OptimizableStructure)Modification.Execute(individual,
                            1.0, 1.0, randomNumberGenerator);

                population.Add(mutatedIndividual.Copy());
                scoreList.Add(double.MinValue);
            }

            mutationRate = optimizationSettings.RelativeMutationProbability /
                        (double)population[0].ParameterList.Count;
        }

        private void SetUpRMHCAlgorithm()
        {
            populationSize = 1;
            creepMutationRate = optimizationSettings.CreepMutationRate;

            int numberOfLineSegments = optimizationSettings.NumberOfRoadSegments;

            double minimumParameterValue = optimizationSettings.MinimumSpeedValue;
            double maximumParameterValue = optimizationSettings.MaximumSpeedValue;
            double discretizationStep = optimizationSettings.SpeedIncrement;
            double desiredAverageSpeed = optimizationSettings.DesiredAverageSpeed;

            possibleSpeedList = new List<double>();
            double initialSpeed = minimumParameterValue;
            possibleSpeedList.Add(initialSpeed);

            while (initialSpeed < maximumParameterValue)
            {
                initialSpeed += discretizationStep;
                initialSpeed = Math.Min(initialSpeed, maximumParameterValue);
                possibleSpeedList.Add(initialSpeed);
            }

            //returns the index of desired average speed from the possible speed list (generated above). Required for optimizaiton.
            int desiredAverageSpeedIndex = possibleSpeedList.FindIndex(s => s == desiredAverageSpeed);

            for (int ii = 0; ii < populationSize; ii++)
            {
                OptimizableStructure individual = new OptimizableStructure();

                IntParameter p0ParameterFirst = new IntParameter();
                p0ParameterFirst.MaximumValue = possibleSpeedList.Count - 1;
                p0ParameterFirst.MinimumValue = 0;
                p0ParameterFirst.ParameterValue = desiredAverageSpeedIndex;
                individual.ParameterList.Add(p0ParameterFirst);

                for (int jj = 0; jj < numberOfLineSegments; jj++)
                {
                    IntParameter p0Parameter = new IntParameter();
                    p0Parameter.MaximumValue = possibleSpeedList.Count - 1;
                    p0Parameter.MinimumValue = 0;
                    p0Parameter.ParameterValue = desiredAverageSpeedIndex;
                    individual.ParameterList.Add(p0Parameter);
                }
                population.Add(individual.Copy());
                scoreList.Add(double.MinValue);
            }

            mutationRate = optimizationSettings.RelativeMutationProbability /
                        (double)population[0].ParameterList.Count;          
        }

        private void OptimizationLoop()
        {
            population = new List<OptimizableStructure>();
            scoreList = new List<double>();

            if (optimizationMethod == OptimizationMethod.GA)
            {
                stopWatch.Start();
                running = true;
                double elapsedOptimizationTime;
                SetUpGeneticAlgorithm();
                creepMutationRate = 0;

                bestScore = double.MinValue;
                iterationIndex = 0;
                numberOfEvaluatedIndividuals = 0;

                double minimumParameterValue = optimizationSettings.MinimumSpeedValue;
                double maximumParameterValue = optimizationSettings.MaximumSpeedValue;
                double desiredAverageSpeed = optimizationSettings.DesiredAverageSpeed;

                while (running)
                {
                    for (int ii = 0; ii < populationSize; ii++)
                    {
                        OptimizableStructure individual = population[ii].Copy();
                        speedProfileEvaluator = new PiecewiseLinearSpeedProfileEvaluator();
                        speedProfileEvaluator.AssignMetricMap(metricMap);
                        speedProfileEvaluator.AssignMetricPath(metricPath);
                        speedProfileEvaluator.MaximumAllowedSpeed = maximumParameterValue;
                        speedProfileEvaluator.MinimumAllowedSpeed = minimumParameterValue;
                        speedProfileEvaluator.DesiredAverageSpeed = desiredAverageSpeed;
                        speedProfileEvaluator.AssignPossibleSpeedList(possibleSpeedList);

                        double score = speedProfileEvaluator.EvaluateGA(individual);
                        scoreList[ii] = score;
                        cumulativeScore += score;

                        List<int> individualSpeedIndexList = new List<int>();
                        for (int kk = 0; kk < individual.ParameterList.Count; kk++)
                        {
                            individualSpeedIndexList.Add(((IntParameter)individual.ParameterList[kk]).ParameterValue);
                        }
                        long speedProfileIndex = ConvertToBase10(individualSpeedIndexList);

                        OnIndividualEvaluated(individualSpeedIndexList, speedProfileIndex, 0, score, individual.Copy(), ii, iterationIndex, true);

                        if (score >= bestScore)
                        {
                            // MW 20180206
                            if (score > bestScore)
                            {
                                OnNewBestIndividualFound(individualSpeedIndexList, speedProfileIndex, 0, score, individual.Copy(), ii, iterationIndex, true);
                            }


                            bestScore = score;
                            bestIndividualIndex = ii;
                            // Copy the best individual here, in case the algorithm exits due to maximum time reached.
                            optimizedSpeedProfile = individual.Copy();
                        }
                        numberOfEvaluatedIndividuals++;
                        elapsedOptimizationTime = stopWatch.ElapsedTicks / (double)Stopwatch.Frequency;
                        if (elapsedOptimizationTime >= optimizationTime | numberOfEvaluatedIndividuals >= optimizationSettings.NumberOfGenerations * optimizationSettings.PopulationSize)
                        {
                            running = false;
                            OnStopped();
                            break;
                        }
                        if (!running) { break; }
                    }

                    // Make new generation:
                    OptimizableStructure bestIndividual = population[bestIndividualIndex].Copy();
                    List<OptimizableStructure> oldPopulation = new List<OptimizableStructure>();
                    foreach (OptimizableStructure individual in population)
                    {
                        OptimizableStructure copiedIndividual = individual.Copy();
                        oldPopulation.Add(copiedIndividual);

                    }
                    population = new List<OptimizableStructure>();
                    int counter = 0;
                    while (counter < oldPopulation.Count)
                    {
                        int firstIndex = TournamentSelection.Select(randomNumberGenerator, scoreList, OptimizationObjective.Maximization,
                            tournamentSize, tournamentSelectionParameter);
                        int secondIndex = TournamentSelection.Select(randomNumberGenerator, scoreList, OptimizationObjective.Maximization,
                            tournamentSize, tournamentSelectionParameter);
                        double r = randomNumberGenerator.NextDouble();
                        OptimizableStructure parent1 = oldPopulation[firstIndex];
                        OptimizableStructure parent2 = oldPopulation[secondIndex];
                        if (r < crossoverProbability)
                        {
                            List<OptimizableStructure> newIndividualsList = null;
                            newIndividualsList = Crossover.ExecuteSinglePoint(parent1, parent2, unitLength, randomNumberGenerator);
                            population.Add(newIndividualsList[0]);
                            population.Add(newIndividualsList[1]);
                        }
                        else
                        {
                            population.Add(parent1);
                            population.Add(parent2);
                        }
                        counter += 2;
                    }
                    if (population.Count > oldPopulation.Count) { population.RemoveAt(population.Count - 1); } // If the population size is odd..

                    for (int jj = 0; jj < population.Count; jj++)
                    {
                        OptimizableStructure individual = population[jj];
                        OptimizableStructure mutatedIndividual = (OptimizableStructure)Modification.Execute(individual,
                            mutationRate, creepMutationRate, randomNumberGenerator);
                        population[jj] = mutatedIndividual;
                    }
                    if (useElitism) { population[0] = bestIndividual; }

                    double bestFitness = bestScore;
                    double averageFitness = scoreList.Average();
                    List<int> bestIndividualSpeedIndexList = new List<int>();
                    for (int kk = 0; kk < bestIndividual.ParameterList.Count; kk++)
                    {
                        bestIndividualSpeedIndexList.Add(((IntParameter)bestIndividual.ParameterList[kk]).ParameterValue);
                    }

                    long bestSpeedProfileIndex = ConvertToBase10(bestIndividualSpeedIndexList);

                    List<int> populationAverageIndividual = new List<int>();

                    OnGenerationEvaluated(bestFitness, averageFitness, iterationIndex, bestIndividualIndex,
                        numberOfEvaluatedIndividuals, bestIndividualSpeedIndexList, bestSpeedProfileIndex);
                    iterationIndex++;
                    if (numberOfEvaluatedIndividuals >= optimizationSettings.NumberOfGenerations * optimizationSettings.PopulationSize)
                    {
                        running = false;
                        OnStopped();
                        break;
                    }
                }
                OnStopped();
            }
            else if(optimizationMethod == OptimizationMethod.RMHC)
            {
                stopWatch.Start();
                running = true;
                double elapsedOptimizationTime;
                SetUpRMHCAlgorithm();

                iterationIndex = 0;

                double minimumParameterValue = optimizationSettings.MinimumSpeedValue;
                double maximumParameterValue = optimizationSettings.MaximumSpeedValue;
                double desiredAverageSpeed = optimizationSettings.DesiredAverageSpeed;

                speedProfileEvaluator = new PiecewiseLinearSpeedProfileEvaluator();
                speedProfileEvaluator.AssignMetricMap(metricMap);
                speedProfileEvaluator.AssignMetricPath(metricPath);
                speedProfileEvaluator.MaximumAllowedSpeed = maximumParameterValue;
                speedProfileEvaluator.MinimumAllowedSpeed = minimumParameterValue;
                speedProfileEvaluator.DesiredAverageSpeed = desiredAverageSpeed;
                speedProfileEvaluator.AssignPossibleSpeedList(possibleSpeedList);

                optimizedSpeedProfile = population[0].Copy();
                bestScore = speedProfileEvaluator.EvaluateGA(optimizedSpeedProfile);

                while (running)
                {
                    OptimizableStructure individual = optimizedSpeedProfile.Copy();
                    OptimizableStructure mutatedIndividual = (OptimizableStructure)Modification.Execute(individual,
                            mutationRate, creepMutationRate, randomNumberGenerator);
                    speedProfileEvaluator = new PiecewiseLinearSpeedProfileEvaluator();
                    speedProfileEvaluator.AssignMetricMap(metricMap);
                    speedProfileEvaluator.AssignMetricPath(metricPath);
                    speedProfileEvaluator.MaximumAllowedSpeed = maximumParameterValue;
                    speedProfileEvaluator.MinimumAllowedSpeed = minimumParameterValue;
                    speedProfileEvaluator.DesiredAverageSpeed = desiredAverageSpeed;
                    speedProfileEvaluator.AssignPossibleSpeedList(possibleSpeedList);

                    numberOfEvaluatedIndividuals++;
                    double score = speedProfileEvaluator.EvaluateGA(mutatedIndividual);
                    cumulativeScore += score;

                    List<int> individualSpeedIndexList = new List<int>();
                    for (int kk = 0; kk < individual.ParameterList.Count; kk++)
                    {
                        individualSpeedIndexList.Add(((IntParameter)individual.ParameterList[kk]).ParameterValue);
                    }
                    long speedProfileIndex = ConvertToBase10(individualSpeedIndexList);

                    OnIndividualEvaluated(individualSpeedIndexList, speedProfileIndex, speedProfileEvaluator.AverageSpeed, score, 
                        mutatedIndividual.Copy(), numberOfEvaluatedIndividuals, numberOfEvaluatedIndividuals, true);

                    if (score > bestScore)
                    {
                        OnNewBestIndividualFound(individualSpeedIndexList, speedProfileIndex, speedProfileEvaluator.AverageSpeed, score,
                        mutatedIndividual.Copy(), numberOfEvaluatedIndividuals, numberOfEvaluatedIndividuals,true);
                        bestScore = score;
                        optimizedSpeedProfile = mutatedIndividual.Copy();
                    }
                    
                    elapsedOptimizationTime = stopWatch.ElapsedTicks / (double)Stopwatch.Frequency;
                    if (elapsedOptimizationTime >= optimizationTime | numberOfEvaluatedIndividuals > optimizationSettings.NumberOfGenerations * optimizationSettings.PopulationSize)
                    {
                        running = false;
                        OnStopped();
                        break;
                    }
                }
            }           
            OnStopped();
        }

        private long ConvertToBase10(List<int> speedIndexList)
        {
            long convertedSpeedIndex = 0;
            int numberOfSpeedLevels = possibleSpeedList.Count;

            for (int ii = 0; ii < speedIndexList.Count; ii++)
            {
                convertedSpeedIndex += speedIndexList[ii] * (long)Math.Pow(numberOfSpeedLevels, ii);
            }

            return convertedSpeedIndex;
        }

        public void Run(PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings,
            Random randomNumberGenerator, double optimizationTime, MetricMap metricMap, MetricPath metricPath)
        {
            this.optimizationTime = optimizationTime;
            this.optimizationSettings = optimizationSettings;
            this.metricMap = metricMap;
            this.metricPath = metricPath;
            this.randomNumberGenerator = randomNumberGenerator;

            stopWatch = new Stopwatch();
            optimizationThread = new Thread(new ThreadStart(OptimizationLoop));
            optimizationThread.Start();
        }

        public void RunSynchronous(PiecewiseLinearSpeedProfileOptimizationSettings optimizationSettings,
            Random randomNumberGenerator, double optimizationTime, MetricMap metricMap, MetricPath metricPath)
        {
            this.optimizationTime = optimizationTime;
            this.optimizationSettings = optimizationSettings;
            this.metricMap = metricMap;
            this.metricPath = metricPath;
            this.randomNumberGenerator = randomNumberGenerator;

            stopWatch = new Stopwatch();
            OptimizationLoop();
        }

        public void Stop()
        {
            if (running)
            {
                running = false;
            }
        }

        public OptimizationMethod OptimizationMethod
        {
            get { return optimizationMethod; }
            set { optimizationMethod = value; }
        }

        public List<double> PossibleSpeedList
        {
            get { return possibleSpeedList; }
        }

        public double BestScore
        {
            get { return bestScore; }
        }

        public double AverageScore
        {
            get { return 0; }
        }

        public int NumberOfEvaluatedIndividuals
        {
            get { return numberOfEvaluatedIndividuals; }
        }

        public OptimizableStructure OptimizedSpeedProfile
        {
            get { return optimizedSpeedProfile; }
        }

        public double AverageFitness
        {
            get { return cumulativeScore / numberOfEvaluatedIndividuals; }
        }
    }
}
