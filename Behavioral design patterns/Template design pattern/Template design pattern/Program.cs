using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_design_pattern
{
    // A high level blueprint for an algorithm to be completed  by inheritors
    // Template method allows us to define the skeleton of the algorithm, with concrete implementations defined in subclasses
    // Define an algorithm at a high level
    // Define continuent parts as abstract methods/properties
    // Inherit the algorithm class, providing necessary overrides
    internal class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();
            chess.Run();
            Console.ReadLine();
        }
    }
    public abstract class Game
    {
        public void Run()
        {
            Start();
            while(!haveWinner)
            {
                takeTurn();
            }
            Console.WriteLine($"Player {WinningPlayer} wins.");
        }
        protected int currentPlayer;
        protected readonly int numberOfPlayers;
        protected abstract void Start();   
        protected Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }
        protected abstract void takeTurn();
        protected abstract bool haveWinner { get; }
        protected abstract int WinningPlayer { get; }
    }
    public class Chess : Game
    {
        public Chess() : base(2)
        {

        }
        protected override bool haveWinner => turn == maxTurns;

        protected override void Start()
        {
            Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
        }

        protected override void takeTurn()
        {
            Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        }
        protected override int WinningPlayer => currentPlayer;
        private int turn = 1;
        private int maxTurns = 10;

    }
}
