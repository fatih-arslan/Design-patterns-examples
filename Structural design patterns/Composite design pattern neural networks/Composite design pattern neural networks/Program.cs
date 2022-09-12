using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_design_pattern_neural_networks
{
    // Objects can use other objects via inheritance/composition
    // Some composed and singular objects need similar/identical behaviours
    // Composite design pattern lets us treat both types of objects uniformly
    // C# has special support for the enumeration concept
    // A single object can masquerade as a collection with 'yield return this'
    internal class Program
    {
        static void Main(string[] args)
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            var layer1 = new NeuronLayer();
            var layer2 = new NeuronLayer();
            neuron1.ConnectTo(layer1);
            layer1.ConnectTo(layer2);
        }
    }
    public class Neuron : IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class NeuronLayer : Collection<Neuron>
    {

    }
    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other) // 'this' keyword allows us to call the method as neuron.ConnectTo(neuron)                                                                                               
        {                                                                                     // instead of ConnecTo(neuron, neuron)
            if (ReferenceEquals(self, other)) return;
            foreach (var from in self)
            {
                foreach(var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }
}
