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
    }
}
