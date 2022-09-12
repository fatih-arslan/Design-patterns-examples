using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Null_design_pattern
{
    // A behavioral design pattern with no behaviour
    // When component A uses component B, it typically assumes that B is not null
    // There is no option of telling A not to use an instance of B
    // Thus we build a no-op, non-functioning inheritor of B and pass it into A
    // Summary:
    // Implement the required interface
    // Rewrite the methods with empty bodies (if method is non-void return default(T))
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var log = new ConsoleLog();
            var ba = new BankAccount(log);
            ba.Deposit(100);
            Console.ReadLine();
        }
    }
    public interface ILog
    {
        void info(string message);
        void warn(string message);
    }
    class ConsoleLog : ILog
    {
        public void info(string message)
        {
            Console.WriteLine(message);
        }

        public void warn(string message)
        {
            Console.WriteLine("WARNING!! " + message);
        }
    }
    public class NullLog : ILog
    {
        public void info(string message)
        {
            
        }

        public void warn(string message)
        {
            
        }
    }
    public class BankAccount
    {
        private ILog log;
        private int balance;
        public BankAccount(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public void Deposit(int amount)
        {
            balance += amount;
            log.info($"Deposited {amount} balance is now {balance}");
        }
    }
}
