using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public class DFSSolver : GameSolver
    {
        private ReachTheFlagGame _game;

        public DFSSolver(ReachTheFlagGame game)
        {
            this._game = game;
        }

        public void Solve()
        {
            if (_game.IsFinal())
            {
                Console.WriteLine("Game is already solved.");
                return;
            }

            GameState initialState = _game.CurrentState;
            Stack<GameState> stateStack = new();

            stateStack.Push(initialState);

            while(stateStack.Count > 0)
            {
                GameState state = stateStack.Pop();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllPossibleStates())
                {
                    GameState stateNode = kvp.Value;

                    if (stateNode.IsFinal())
                    {
                        Printer.PrintBoard(stateNode.Board);
                        Console.WriteLine("Game done.");

                        return;
                    }

                    else
                    {
                        stateStack.Push(stateNode);
                    }
                }
            }

            Console.WriteLine("Game is impossible to solve.");
        }
    }
}
