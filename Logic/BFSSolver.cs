using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public class BFSSolver : GameSolver
    {
        private ReachTheFlagGame _game;

        public BFSSolver(ReachTheFlagGame game)
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
            Queue<GameState> stateQueue = new();

            stateQueue.Enqueue(initialState);

            while (stateQueue.Count > 0)
            {
                GameState state = stateQueue.Dequeue();

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
                        stateQueue.Enqueue(stateNode);
                    }
                }
            }

            Console.WriteLine("Game is impossible to solve.");
        }
    }
}
