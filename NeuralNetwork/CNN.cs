using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class CNN : IVectorFunction
    {
        private List<Layer> layers_;

        public CNN(IList<int> layers, int seed = 123)
        {
            Neuron.InitRandom(seed);
            layers_ = new List<Layer>(layers.Count);
            layers_.Add(new Layer(layers[0], 0));
            for (int i = 1; i < layers.Count; i++)
            {
                layers_.Add(new Layer(layers[i], layers_[i-1].Count));
            }
            Neuron.FreeRandom();
        }

        public void SetInputValues(IList<double> values)
        {
            layers_[0].SetValues(values);
        }

        public List<double> GetOutputValues()
        {
            return layers_[layers_.Count - 1].GetValues();
        }

        public void Evaluate()
        {
            for (int i = 1; i < layers_.Count; i++)
                layers_[i].Evaluate(layers_[i - 1]);
        }

        public IList<double> Evaluate(IList<double> inputs)
        {
            SetInputValues(inputs);          
            Evaluate();
            return GetOutputValues();
        }
    }
}
