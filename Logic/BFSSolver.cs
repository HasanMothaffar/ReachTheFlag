using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public class BFSSolver : GameSolver
    {
        public BFSSolver(GameState initialNode) : base("BFS", initialNode) { }

        public override void Solve()
        {
            GameState initialState = this.InitialNode;
            Queue<GameState> stateQueue = new();

            stateQueue.Enqueue(initialState);

            while (stateQueue.Count > 0)
            {
                GameState state = stateQueue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllNeighboringStates())
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

            throw new Exception("Game is impossible to solve.");
        }
    }
}
