using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Neuron
    {
        private List<double> weights_;

        private double bias_;

        // Current value of the neuron
        private double value_;
        public double Value
        {
            get { return value_; }
            set
            {
                if (weights_.Count > 0)
                    throw new ArgumentException($"Set value not on an input Neuron");
                value_ = value;
            }
        }

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

        public Neuron(int size, bool randomize)
        {
            weights_ = new List<double>(size);
            if (randomize)
            {
                for (int i = 0; i < size; i++)
                    weights_.Add(GetRandomDouble());
                bias_ = GetRandomDouble();
            }
        }

        private double GetRandomDouble(double min = -1.0, double max = +1.0)
        {
            return min + (max - min) * random.NextDouble();
        }

        public void Evaluate(Layer previousLayer)
        {
            if (weights_.Count != previousLayer.Count)
                throw new ArgumentException($"Weight size do not match: {weights_.Count} {previousLayer.Count}");
            // calculate W.X + B
            double sum = bias_;
            for (int i = 0; i < weights_.Count; i++)
                sum += weights_[i] * previousLayer.NeuronValue(i);
            // Non linear function
            value_ = Helpers.Sigmoid(sum);
        }
    }
}
