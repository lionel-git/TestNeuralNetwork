using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Neuron
    {
        public List<double> Weights { get; set; }

        public double Bias { get; set; }

        public double Value { get; set; }

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
        }

        public Neuron(int size, bool randomize)
        {
            Weights = new List<double>(size);
            if (randomize)
            {
                for (int i = 0; i < size; i++)
                    Weights.Add(GetRandomDouble());
                Bias = GetRandomDouble();
            }
            else
            {
                for (int i = 0; i < size; i++)
                    Weights.Add(0.0);
                Bias = 0.0;
            }
        }

        private double GetRandomDouble(double min = -1.0, double max = +1.0)
        {
            return min + (max - min) * random.NextDouble();
        }

        public void Evaluate(Layer previousLayer)
        {
            if (Weights.Count != previousLayer.Count)
                throw new ArgumentException($"Weight size do not match: {Weights.Count} {previousLayer.Count}");
            // calculate W.X + B
            double sum = Bias;
            for (int i = 0; i < Weights.Count; i++)
                sum += Weights[i] * previousLayer.NeuronValue(i);
            // Non linear function
            Value = Helpers.Sigmoid(sum);
        }
    }
}
