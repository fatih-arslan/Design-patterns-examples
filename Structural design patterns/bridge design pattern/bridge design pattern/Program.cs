using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bridge_design_pattern
{
    // Connecting components through abstractions
    // Bridge prevents a 'Cartesian Product' complexity exploison
    // Bridge avoids the entity explosion
    // Decouple abstraction from implementation
    // Both can exist as hierarchies
    // A stronger from of encapsulation
    internal class Program
    {
        static void Main(string[] args)
        {
            //IRenderer renderer = new RasterRenderer();
            IRenderer renderer = new VectorRenderer();
            var circle = new Circle(renderer, 5);
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
            Console.ReadLine();
        }
    }
    public interface IRenderer // this interface is used as a bridge between objects and different types of renderers
    {                         //  instead of writing draw methods for every type of renderer in shape classes we use a bridge between shapes and renderers
        void RenderCircle(float radius);
    }
    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }
    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle of radius {radius}");
        }
    }
    public abstract class Shape
    {
        protected IRenderer renderer;
        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public abstract void Draw();
        public abstract void Resize(float factor);
    }
    public class Circle : Shape
    {
        private float radius;
        public Circle(IRenderer renderer, float radius) : base(renderer) // : base(renderer) is used to give the renderer parameter to the base class' constructor method
        {
            this.radius = radius;
        }
        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }
}
