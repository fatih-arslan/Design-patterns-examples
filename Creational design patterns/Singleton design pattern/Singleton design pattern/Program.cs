using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton_design_pattern
{
    // Used when the constructor call is expensive and you only want to use it once
    // Making a 'safe' singleton is easy: construct a static Lazy<T> and return its value
    // Singletons are difficult to test
    // Instead of directly using a singleton, consider depending on an abstraction (e.g. an interface)
    // Consider defining singleton lifetime not explicitly in the component but instead in dependency injection container
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
        }
    }
    public interface IDatabase
    {
        int GetPopulation();
    }
    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private SingletonDatabase()
        {
            //todo
        }
        public int GetPopulation()
        {
            return 5;
        }        

        public static SingletonDatabase instance = new SingletonDatabase();
        public static SingletonDatabase Instance => instance;
    }
}
