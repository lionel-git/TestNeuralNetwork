using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NeuralNetwork
{
    public class Neuron
    {
        private List<double> weights_;

        private double bias_;

        private static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();

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

        private double GetRandomDouble(double min = -10.0, double max = +10.0)
        {
            var bytes = new Byte[8];
            random.GetBytes(bytes);
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            var value = ul / (Double)(1UL << 53); // Should be in [0,1[
            return min + (max - min) * value;
        }
    }
}
