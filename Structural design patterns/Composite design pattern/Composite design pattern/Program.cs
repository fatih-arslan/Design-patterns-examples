using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite_design_pattern
{
    internal class Program
    {
        // Objects can use other objects via inheritance/composition
        // Some composed and singular objects need similar/identical behaviours
        // Composite design pattern lets us treat both types of objects uniformly
        // C# has special support for the enumeration concept
        // A single object can masquerade as a collection with 'yield return this'
        static void Main(string[] args)
        {
            var drawing = new GraphicObject { Name = "My Drawing"};
            drawing.Children.Add(new Square { Color = "Red" });
            var group = new GraphicObject();
            group.Children.Add(new Circle { Color = "Blue" });
            group.Children.Add(new Square { Color = "Blue" });
            drawing.Children.Add(group);
            drawing.Children.Add(new Circle { Color = "Yellow" });

            
            Console.WriteLine(drawing);
            Console.ReadLine();
        }
    }
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color;
        private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => children.Value;
        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth)).Append(String.IsNullOrWhiteSpace(Color) ? String.Empty : $"{Color} ").AppendLine(Name);
            foreach(var child in Children)
            {
                child.Print(sb, depth + 1);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }
    public class Circle : GraphicObject
    {
        public override string Name => "Circle";
    }
    public class Square : GraphicObject
    {
        public override string Name => "Square";
    }
}
