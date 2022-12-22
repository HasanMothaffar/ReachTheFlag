using Raylib_cs;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.UI;

namespace ReachTheFlag.Logic.Solvers.KeyboardSolvers
{
    public class RaylibUserInputSolver : GameSolver
    {
        private readonly Dictionary<KeyboardKey, MoveDirection> _moveDirections = new()
        {
            { KeyboardKey.KEY_A, MoveDirection.Left },
            { KeyboardKey.KEY_D, MoveDirection.Right },
            { KeyboardKey.KEY_W, MoveDirection.Up },
            { KeyboardKey.KEY_S, MoveDirection.Down },
        };

        public RaylibUserInputSolver(GameState initialNode, GameUI ui) : base("User Input", initialNode, ui)
        {
            Statistics.PlayerPath.AddCell(InitialNode.PlayerCell);
            Statistics.FinalState = InitialNode;
        }

        public override GameStatus Solve()
        {
            Raylib.DrawText("Controls: WASD", 12, 64, 24, Color.BLACK);
            respondToUserInput();
            // Don't change the order of these conditions!
            // If the game is final, the player is technically considered stuck.
            if (InitialNode.IsFinal())
            {
                return GameStatus.Win;
            }

            if (InitialNode.IsPlayerStuck())
            {
                return GameStatus.PlayerIsStuck;
            }

            return GameStatus.Pending;
        }

        private void respondToUserInput()
        {
            KeyboardKey keyPressed = (KeyboardKey)Raylib.GetKeyPressed();

            if (_moveDirections.ContainsKey(keyPressed))
            {
                MoveDirection direction = _moveDirections[keyPressed];

                InitialNode.MovePlayerToDirection(direction);
                Statistics.PlayerPath.AddCell(InitialNode.PlayerCell);
                Statistics.NumberOfPlayerMoves++;
            }
        }
    }
}
