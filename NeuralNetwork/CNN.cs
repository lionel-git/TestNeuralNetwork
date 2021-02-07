using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class CNN : IVectorFunction
    {
        private List<Layer> layers_;

        public CNN(IList<int> layers)
        {
            layers_ = new List<Layer>(layers.Count);
            layers_.Add(new Layer(layers[0], 0));
            for (int i = 1; i < layers.Count; i++)
            {
                layers_.Add(new Layer(layers[i], layers_[i-1].Count));
            }
        }

        public IList<double> Evaluate(IList<double> inputs)
        {
            throw new NotImplementedException();
        }
    }
}
