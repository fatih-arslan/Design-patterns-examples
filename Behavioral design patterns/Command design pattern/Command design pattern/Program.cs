using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_design_pattern
{
    // Command pattern is something that lets you accomplish a multilevel undo redo functionality
    // Want an object that represents an instruction to perform a particular action.
    // Encapsulate all details of an operation in a seperate object
    // Define instruction for applying the command 
    // Optionally define instructions for undoing the command
    internal class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50)
            };
            Console.WriteLine(ba);
            foreach(var command in commands)
            {
                command.Call();
            }
            Console.WriteLine(ba);
            foreach(var command in Enumerable.Reverse(commands))
            {
                command.Undo();
            }
            Console.Write(ba);
            Console.ReadLine();
        }
    }
    public class BankAccount
    {
        private int balance;
        private int overDraftLimit = -500;
        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount} balance is now {balance}");
        }        
        public bool Withdraw(int amount) // Withdraw method returns a boolean value so that we can see if withdraw was successfull                                         
        {                                // beacuse undoing a failed withdrawing process would add extra money to the balance 
            if (balance - amount >= overDraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount} balance is now {balance}");
                return true;
            }            
            return false;
        }
        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }
    public interface ICommand
    {
        void Call();
        void Undo();
    }
    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        public enum Action
        {
            Deposit, Withdraw
        }
        private Action action;
        private int amount;
        private bool succeeded;
        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch(action)
            {
                case Action.Deposit: 
                    account.Deposit(amount); 
                    succeeded = true; // Because deposits always succeed
                    break;
                case Action.Withdraw:
                    account.Withdraw(amount);
                    succeeded = account.Withdraw(amount); // Succeeded depends on the withdraw function
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!succeeded) return;
            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
