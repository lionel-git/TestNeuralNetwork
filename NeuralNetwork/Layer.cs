using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class Layer
    {
        public List<Neuron> Neurons { get; set; }

        [JsonIgnore]
        public int Count => Neurons.Count;

        public Layer()
        {
            Neurons = new List<Neuron>();
        }

        public Layer(int size, int sizeWeights)
        {
            Neurons = new List<Neuron>(size);
            for (int i = 0; i < size; i++)
                Neurons.Add(new Neuron(sizeWeights, true));
        }
        public void SetValues(IList<double> values)
        {
            if (Neurons.Count != values.Count)
                throw new ArgumentException($"Invalid vector sizes: layerSize={Neurons.Count} inputSize={values.Count}");
            for (int i = 0; i < Neurons.Count; i++)
                Neurons[i].Value = values[i];
        }

        public void Evaluate(Layer previousLayer)
        {
            for (int i = 0; i < Neurons.Count; i++)
                Neurons[i].Evaluate(previousLayer);
        }

        public double NeuronValue(int i)
        {
            return Neurons[i].Value;
        }

        public List<double> GetValues()
        {
            var values = new List<double>(Neurons.Count);
            for (int i = 0; i < Neurons.Count; i++)
                values.Add(Neurons[i].Value);
            return values;
        }
    }
}
