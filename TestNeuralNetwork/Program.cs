using System;
using System.Collections.Generic;
using NeuralNetwork;

namespace TestNeuralNetwork
{
    class Program
    {
        static void Test1()
        {
            var layerSizes = new List<int>() { 10, 5, 5 };
            var cnn = new CNN(layerSizes, 123);
            var inputs = new List<double>() { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
            var outputs = cnn.Evaluate(inputs);
            Helpers.DisplayList("Test1", outputs);
        }

        static void TestCalibration()
        {
            var layerSizes = new List<int>() { 10, 5, 5 };
            var cnnRef = new CNN(layerSizes, 123);
            var cnnTest = new CNN(layerSizes, 456);
            var inputs = new List<double>() { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
            
            var outputsRef = cnnRef.Evaluate(inputs);
            Helpers.DisplayList("Ref", outputsRef);

            var outputsTest = cnnTest.Evaluate(inputs);
            Helpers.DisplayList("Test", outputsTest);
        }


        static void Main(string[] args)
        {
            Test1();
            TestCalibration();
        }
    }
}
