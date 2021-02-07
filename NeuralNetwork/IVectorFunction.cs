using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    interface IVectorFunction
    {
        IList<double> Evaluate(IList<double> inputs);
    }
}
