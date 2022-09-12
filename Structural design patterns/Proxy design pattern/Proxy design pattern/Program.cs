using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_design_pattern
{
    internal class Program
    {
        // An interface for accessing a particular resource
        // A class that functions as an interface to a particular resource. That resource may be remote, expensive construct,
        // or may require logging or some other added functionality
        // To create a proxy simply replicate the existing interface of an object
        // Add relevant functionality to the redefined member functions
        static void Main(string[] args)
        {
            ICar car = new CarProxy(new Driver(18));
            car.Drive();
            Console.ReadLine();
        }      
    }
    // Protection proxy
    public interface ICar
    {
        void Drive();       
    }
    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }
    public class Driver
    {
        public int Age { get; set; }
        public Driver(int age)
        {
            this.Age = age;
        }
    }
    public class CarProxy : ICar
    {
        private Driver driver;
        private Car car = new Car();
        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }
        public void Drive()
        {
            if(driver.Age >= 16)
            {
                car.Drive();
            }
            else
            {
                Console.WriteLine("Too young");
            }
        }
    }
}
