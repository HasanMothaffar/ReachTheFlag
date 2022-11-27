﻿using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
	public class UserInputSolver : GameSolver
	{
        private int _numberOfPlayerMoves = 0;

        private readonly Dictionary<string, MoveDirection> _moveDirections = new()
		{
			{ "a", MoveDirection.Left },
			{ "d", MoveDirection.Right },
			{ "w", MoveDirection.Up },
			{ "s", MoveDirection.Down },
		};

        public UserInputSolver(GameState initialNode) : base("User Input", initialNode) 
        {
            this.PlayerPath.AddCell(InitialNode.PlayerCell);
        }

        public override void Solve()
		{
            char pressedKey;
            Printer.PrintBoard(InitialNode.Board);

            while (true)
            {
                pressedKey = Console.ReadKey(true).KeyChar;
                Console.Clear();
                respondToUserInput(pressedKey);
                Printer.PrintBoard(InitialNode.Board);

                // Don't change the order of these conditions!
                // If the game is final, the player is technically considered stuck.
                if (InitialNode.IsFinal())
                {
                    Console.WriteLine("You won!");
                    break;
                }

                if (InitialNode.IsPlayerStuck())
                {
                    Console.WriteLine("Player is stuck.");
                    break;
                }
            }
		}

		private void respondToUserInput(char pressedKey)
		{
            string lowerCaseKey = pressedKey.ToString().ToLower();

            if (this._moveDirections.ContainsKey(lowerCaseKey))
            {
                MoveDirection direction = this._moveDirections[lowerCaseKey];
                Console.WriteLine($"Move direction: {direction}");

                this.InitialNode.ShiftPlayerPosition(direction);
                this.PlayerPath.AddCell(this.InitialNode.PlayerCell);
                this._numberOfPlayerMoves++;
            }
        }

        protected override void PrintSpecificStatistics()
        {
            Console.WriteLine($"Number of moves: {_numberOfPlayerMoves}");
        }
    }
}