using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memento_design_pattern
{
    internal class Program
    {
        // Keep a memento of an object's state to return to that state
        // An object or system goes through changes (e.g. bank account gets deposits and withdraws)
        // One way to navigate those changes is to record every change (command) and teach a command to undo itself
        // Another is to simply save snapshots of the system
        // A Memento is simply a token/handle class with (typically) no functions of its own
        static void Main(string[] args)
        {
            var ba = new BankAccount(100);
            ba.Deposit(50);
            ba.Deposit(25);
            Console.WriteLine(ba);
            ba.Undo();
            Console.WriteLine($"undo1: {ba}");
            ba.Undo();
            Console.WriteLine($"undo2: {ba}");
            ba.Redo();
            Console.WriteLine($"redo: {ba}");
            Console.Read();
        }
    }
    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            Balance = balance;
        }
    }
    public class BankAccount
    {
        private int balance;
        private List<Memento> changes = new List<Memento>();
        private int current;
        public BankAccount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
        }
        public Memento Deposit(int amount)
        {
            balance += amount;
            var m = new Memento(balance);
            changes.Add(m);
            ++current;
            return m;
        }
        public Memento Restore(Memento m)
        {
            if(m != null)
            {
                balance = m.Balance;
                changes.Add(m);
                return m;
            }
            return null;
        }
        public Memento Undo()
        {
            if(current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }
            return null;
        }
        public Memento Redo()
        {
            if(current + 1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }
            return null;
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }
}
