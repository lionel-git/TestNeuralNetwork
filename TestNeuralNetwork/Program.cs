using System;
using System.Collections.Generic;
using NeuralNetwork;

namespace TestNeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            var layerSizes = new List<int>() { 10, 5, 5 };
            var cnn = new CNN(layerSizes);
        }
    }
}
