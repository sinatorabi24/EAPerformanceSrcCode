using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StochasticOptimizationLibrary.Parameters;

namespace EAPerformanceApplication.AddedClasses.Optimization
{
    public class IntParameter : OptimizableParameter
    {
        private int parameterValue;
        private int minimumValue;
        private int maximumValue;
        private int increment = 1; // Default value for IntParameter

        // MW ToDo: Add creep mutations as an option...
        public override void Modify(double modificationProbability, double creepProbability, Random randomNumberGenerator)
        {
            double r = randomNumberGenerator.NextDouble();
            if (r < modificationProbability)
            {
                double s = randomNumberGenerator.NextDouble();
                if (s < creepProbability)
                {
                    int delta = randomNumberGenerator.Next(-increment, increment + 1);
                    parameterValue += delta;
                    if (parameterValue > maximumValue) { parameterValue = maximumValue; }
                    else if (parameterValue < minimumValue) { parameterValue = minimumValue; }
                }
                else
                {
                    parameterValue = randomNumberGenerator.Next(minimumValue, maximumValue + 1);
                }
            }
        }

        public override OptimizableParameter Copy()
        {
            IntParameter copiedParameter = new IntParameter();
            copiedParameter.ParameterValue = this.parameterValue;
            copiedParameter.MinimumValue = this.minimumValue;
            copiedParameter.MaximumValue = this.maximumValue;
            copiedParameter.Increment = this.increment;
            return copiedParameter;
        }

        public override dynamic GetValue()
        {
            return parameterValue;
        }

        public override void SetValue(dynamic value)
        {
            parameterValue = (int)value;
        }

        public int ParameterValue
        {
            get { return parameterValue; }
            set { parameterValue = value; }
        }

        public int MinimumValue
        {
            get { return minimumValue; }
            set { minimumValue = value; }
        }

        public int MaximumValue
        {
            get { return maximumValue; }
            set { maximumValue = value; }
        }

        public int Increment
        {
            get { return increment; }
            set { increment = value; }
        }
    }
}
