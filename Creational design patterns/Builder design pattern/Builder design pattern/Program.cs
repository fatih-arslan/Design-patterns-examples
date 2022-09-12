using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_design_pattern
{
    // Allows us to create an object piece by piece
    // A Builder is a seperate component for building an object
    // You can either give builder a constructor or return it via a static function
    // To make Builder fluent return 'this'
    // Different facets of an object can be built with different builders working in tandem via a base class
    internal class Program
    {
        static void Main(string[] args)
        {
            var car = CarBuilder.Create().OfType(CarType.CrossOver).WithWheels(18).Build();
        }
    }
    //Stepwise Builder design pattern 
    public enum CarType
    {
        Sedan, CrossOver
    }
    public class Car
    {
        public CarType Type;
        public int WheelSize;
    }
    public interface ISpecifyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }
    public interface ISpecifyWheelSize
    {
        IBuildCar WithWheels(int size);
    }
    public interface IBuildCar
    {
        Car Build();
    }
    public class CarBuilder
    {
        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car car = new Car();
            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }
            public IBuildCar WithWheels(int size)
            {
                car.WheelSize = size;
                return this;
            }
            public Car Build()
            {
                return car;
            }
        }
    }
}
