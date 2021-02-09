using System;
using System.Collections.Generic;
using NeuralNetwork;
using MNIST;
using System.IO;
using Common;

namespace TestNeuralNetwork
{
    class Program
    {
        static void Test1()
        {
            string path = "cnn1.json";
            var layerSizes = new List<int>() { 10, 5, 5 };
            var cnn = new CNN(layerSizes, 123);
            var inputs = new List<double>() { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
            var outputs = cnn.Evaluate(inputs);
            Helpers.DisplayList("Test1", outputs);

            // Check Save + Reload
            var cnnString = cnn.ToJson(false);
            cnn.Save(path);
            var cnn2 = CNN.Load(path);
            var cnn2String = cnn2.ToJson(false);
            if (cnnString == cnn2String)
                Console.WriteLine("Save/load ok!");
            else
                throw new Exception("Cnn save/load error");
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

        static void TestMnist()
        {
            var mnsitReader = new MnistReader();
            var data = mnsitReader.LoadSample(@"g:\MNIST\train-images-idx3-ubyte.gz");
            Console.WriteLine(data.Length);
            var data2 = File.ReadAllBytes(@"G:\MNIST\Check\train-images.idx3-ubyte");
            
            if (data.Length != data2.Length)
                throw new Exception("Size do not match");
            for (int i=0;i<data.Length;i++)
                if (data[i]!=data2[i])
                    throw new Exception($"data do not match (i={i})");
            Console.WriteLine("File is ok");
        }

        static void TestMnist2()
        {
            var mnsitReaderTest = new MnistReader();
            mnsitReaderTest.LoadImagesAndLabels(@"g:\MNIST\t10k-images-idx3-ubyte.gz", @"g:\MNIST\t10k-labels-idx1-ubyte.gz");
            Console.WriteLine($"Loaded: {mnsitReaderTest.ImagesLabels.Count}");

            var mnsitReaderTrain = new MnistReader();
            mnsitReaderTrain.LoadImagesAndLabels(@"g:\MNIST\train-images-idx3-ubyte.gz", @"g:\MNIST\train-labels-idx1-ubyte.gz");
            Console.WriteLine($"Loaded: {mnsitReaderTrain.ImagesLabels.Count}");

        }

        static void Main(string[] args)
        {
            try
            {
                TestMnist2(); return;
                TestMnist();
                Test1();
                TestCalibration();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
