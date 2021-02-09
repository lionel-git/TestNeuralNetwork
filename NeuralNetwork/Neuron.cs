using System;
using System.Collections.Generic;
using Common;

namespace NeuralNetwork
{
    public class Neuron
    {
        // Weigth(n) = bias, Current weights, updated on backward
        public List<double> Weights { get; set; }
       
        // Current "values", updated on forward
        public double Value { get; set; }       
        public List<double> WeightsDerivative { get; set; }        
        

        private static Random random = null;

        public static void InitRandom(int seed)
        {
            if (random == null)
                random = new Random(seed);
            else
                throw new Exception($"Random seed already initialised");
        }

        public static void FreeRandom()
        {
            random = null;
        }

        public Neuron()
        {
            Weights = new List<double>();
            WeightsDerivative = new List<double>();
        }

        public Neuron(int size, bool randomize)
        {
            Weights = new List<double>(size + 1);
            WeightsDerivative = new List<double>(size + 1);
            if (randomize)
            {
                for (int i = 0; i <= size; i++)
                {
                    Weights.Add(GetRandomDouble());
                    WeightsDerivative.Add(0.0);
                }
            }
            else
            {
                for (int i = 0; i <= size; i++)
                {
                    Weights.Add(0.0);
                    WeightsDerivative.Add(0.0);
                }
            }
        }

        private double GetRandomDouble(double min = -1.0, double max = +1.0)
        {
            return min + (max - min) * random.NextDouble();
        }

        public void Evaluate(Layer previousLayer)
        {
            if (Weights.Count != previousLayer.Count + 1)
                throw new ArgumentException($"Weight size do not match: {Weights.Count} {previousLayer.Count}");
            // calculate B + W.X
            double sum = Weights[previousLayer.Count];
            for (int i = 0; i < previousLayer.Count; i++)
                sum += Weights[i] * previousLayer.NeuronValue(i);
            
            // Non linear function            
            Helpers.Sigmoid(sum, out double value, out double SumDerivative);

            // Derive de Value par rapport au weights et bias
            Value = value;
            WeightsDerivative[previousLayer.Count] = SumDerivative;
            for (int i = 0; i < previousLayer.Count; i++)
                WeightsDerivative[i] = SumDerivative * previousLayer.NeuronValue(i);
        }
    }
}
