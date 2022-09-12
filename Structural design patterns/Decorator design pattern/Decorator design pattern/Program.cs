using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator_design_pattern
{
    // Adding behaviour without altering the class itself
    // A decorator keeps the reference to the decorated objects
    internal class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Fly();
            d.Crawl();
            d.Weight = 5;
            Console.ReadLine();
        }
    }

    public interface IBird
    {
        int Weight { get; set; }

        void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            Console.WriteLine("Soaring in the sky");
        }
    }

    public interface ILizard
    {
        int Weight { get; set; }

        void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            Console.WriteLine("Crawling in the dirt");
        }
    }
    public class Dragon : IBird, ILizard // We use interfaces to be able to inherit from multiple classes
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();

        private int weight;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
        }

        public void Crawl()    // We dont define the body of Crawl and Fly methods again instead we use the methods of Bird and Lizard classes so that                          
        {                     //if we need to change it, we wont have to make changes everywhere we use them.
            lizard.Crawl();
        }

        public void Fly()
        {
            bird.Fly();
        }
    }
}
