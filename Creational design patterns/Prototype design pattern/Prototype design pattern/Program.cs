using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_design_pattern
{
    // Used when it is easier to copy an existing object to fully initialize a new one
    // To implement a protoype, partially construct an object and store it somewhere
    // Clone the prototype (Implement your own deep copy functionality or serialize and deserialize)
    // Customize the resulting instance
    internal class Program
    {
        static void Main(string[] args)
        {
            var john = new Person("John", new Address("123 London Road", "London", "UK"));

            var chris = john.DeepCopy();

            chris.Name = "Chris";
            Console.WriteLine(john); 
            Console.WriteLine(chris);
            Console.ReadLine();
        }
    }
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    public class Address : IPrototype<Address>
    {
        public string StreetAddress, City, Country;
        public Address(string street, string city, string country)
        {
            this.StreetAddress = street;
            this.City = city;
            this.Country = country;
        }       
        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(City)}: {City}, {nameof(Country)}: {Country}";
        }

        public Address DeepCopy()
        {
            return new Address(this.StreetAddress, this.City, this.Country);
        }
    }    
    public class Person : IPrototype<Person>
    {
        public string Name;
        public Address Address;
        public Person(string name, Address adress)
        {
            this.Name = name;
            this.Address = adress;
        }        
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }

        public Person DeepCopy()
        {
            return new Person(Name, Address.DeepCopy());
        }
    }
}
