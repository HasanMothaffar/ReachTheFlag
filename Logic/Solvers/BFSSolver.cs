using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public class BFSSolver : GraphBasedSolver
    {
        public BFSSolver(GameState initialNode) : base("BFS", initialNode) { }

        public override void Solve()
        {
            GameState? finalState = null;

            Queue<GameState> stateQueue = new();
            stateQueue.Enqueue(this.InitialNode);

            bool shouldQuitLoop = false;

            while (stateQueue.Count > 0)
            {
                if (shouldQuitLoop) break;
                GameState state = stateQueue.Dequeue();

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
                        stateQueue.Enqueue(stateNode);
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
