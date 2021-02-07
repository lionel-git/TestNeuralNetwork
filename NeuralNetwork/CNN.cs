using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace NeuralNetwork
{
    public class CNN : IVectorFunction
    {
        public List<Layer> Layers { get; set; }

        public int RandomSeed { get; set; }

        public CNN()
        {
            Layers = new List<Layer>();
        }

        public CNN(IList<int> layers, int seed = 123)
        {
            RandomSeed = seed;
            Neuron.InitRandom(RandomSeed);
            Layers = new List<Layer>(layers.Count);
            Layers.Add(new Layer(layers[0], 0));
            for (int i = 1; i < layers.Count; i++)
            {
                Layers.Add(new Layer(layers[i], Layers[i-1].Count));
            }
            Neuron.FreeRandom();
        }

        public void SetInputValues(IList<double> values)
        {
            Layers[0].SetValues(values);
        }

        public List<double> GetOutputValues()
        {
            return Layers[Layers.Count - 1].GetValues();
        }

        public void Evaluate()
        {
            for (int i = 1; i < Layers.Count; i++)
                Layers[i].Evaluate(Layers[i - 1]);
        }

        public IList<double> Evaluate(IList<double> inputs)
        {
            SetInputValues(inputs);          
            Evaluate();
            return GetOutputValues();
        }

        public string ToJson(bool indented)
        {
            var formatting = indented ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(this, formatting);
        }

        public void Save(string path)
        {
            File.WriteAllText(path, ToJson(true));
        }

        public static CNN Load(string path)
        {
            return JsonConvert.DeserializeObject<CNN>(File.ReadAllText(path));
        }

        public override string ToString()
        {
            return ToJson(true);
        }
    }
}
