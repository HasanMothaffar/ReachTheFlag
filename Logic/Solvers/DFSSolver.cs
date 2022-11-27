using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public class DFSSolver : GraphBasedSolver
    {
        public DFSSolver(GameState initialNode) : base("DFS", initialNode) { }

        public override void Solve()
        {
            GameState? finalState = null;

            Stack<GameState> stateQueue = new();
            stateQueue.Push(this.InitialNode);

            bool shouldQuitLoop = false;

            while (stateQueue.Count > 0)
            {
                if (shouldQuitLoop) break;

                GameState state = stateQueue.Pop();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllNeighboringStates())
                {
                    GameState stateNode = kvp.Value;
                    Parents[stateNode] = state;

                    if (stateNode.IsFinal())
                    {
                        finalState = stateNode;
                        shouldQuitLoop = true;
                        break;
                    }

                    else
                    {
                        stateQueue.Push(stateNode);
                    }
                }
            }

            if (finalState is null)
            {
                throw new Exception("Game is impossible to solve.");
            }

            this.FinalState = finalState;
        }
    }
}
