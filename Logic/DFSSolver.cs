using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public class DFSSolver : GameSolver
    {
        public DFSSolver(GameState initialNode) : base("DFS", initialNode) { }

        public override void Solve()
        {
            GameState initialState = this.InitialNode;
            Stack<GameState> stateStack = new();

            stateStack.Push(initialState);

            while(stateStack.Count > 0)
            {
                GameState state = stateStack.Pop();

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
                        stateStack.Push(stateNode);
                    }
                }
            }

            throw new Exception("Game is impossible to solve.");
        }
    }
}
