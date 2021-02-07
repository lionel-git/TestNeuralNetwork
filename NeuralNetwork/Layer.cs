using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class Layer
    {
        private List<Neuron> neurons_;

        public int Count => neurons_.Count;

        public Layer(int size, int sizeWeights)
        {
            neurons_ = new List<Neuron>(size);
            for (int i = 0; i < size; i++)
                neurons_.Add(new Neuron(sizeWeights, true));
        }
        public void SetValues(IList<double> values)
        {
            if (neurons_.Count != values.Count)
                throw new ArgumentException($"Invalid vector sizes: layerSize={neurons_.Count} inputSize={values.Count}");
            for (int i = 0; i < neurons_.Count; i++)
                neurons_[i].Value = values[i];
        }

        public void Evaluate(Layer previousLayer)
        {
            for (int i = 0; i < neurons_.Count; i++)
                neurons_[i].Evaluate(previousLayer);
        }

        public double NeuronValue(int i)
        {
            return neurons_[i].Value;
        }

        public List<double> GetValues()
        {
            var values = new List<double>(neurons_.Count);
            for (int i = 0; i < neurons_.Count; i++)
                values.Add(neurons_[i].Value);
            return values;
        }
    }
}
