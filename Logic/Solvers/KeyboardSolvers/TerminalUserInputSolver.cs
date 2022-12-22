using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.UI;

namespace ReachTheFlag.Logic.Solvers.KeyboardSolvers
{
    public class TerminalUserInputSolver : GameSolver
    {
        private readonly Dictionary<string, MoveDirection> _moveDirections = new()
        {
            { "a", MoveDirection.Left },
            { "d", MoveDirection.Right },
            { "w", MoveDirection.Up },
            { "s", MoveDirection.Down },
        };

        public TerminalUserInputSolver(GameState initialNode, GameUI ui) : base("User Input", initialNode, ui)
        {
            Statistics.FinalState = InitialNode;
        }

        public override GameStatus Solve()
        {
            UserInterface.DisplayBoard(InitialNode.Board);

            while (true)
            {
                readAndRespondToUserInput();
                UserInterface.DisplayBoard(InitialNode.Board);

                // Don't change the order of these conditions!
                // If the game is final, the player is technically considered stuck.
                if (InitialNode.IsFinal())
                {
                    return GameStatus.Win;
                }

                if (InitialNode.IsPlayerStuck())
                {
                    return GameStatus.ImpossibleToWin;
                }
            }
        }

        private void readAndRespondToUserInput()
        {
            char pressedKey = Console.ReadKey(true).KeyChar;
            string lowerCaseKey = pressedKey.ToString().ToLower();
            Console.Clear();

            if (_moveDirections.ContainsKey(lowerCaseKey))
            {
                MoveDirection direction = _moveDirections[lowerCaseKey];
                Console.WriteLine($"Move direction: {direction}");

                InitialNode.MovePlayerToDirection(direction);
                Statistics.NumberOfPlayerMoves++;
            }
        }
    }
}
