using ReachTheFlag.Exceptions;
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

            Stack<GameState> stack = new();
            stack.Push(this.InitialNode);

            bool shouldQuitLoop = false;

            while (stack.Count > 0)
            {
                NumberOfVisitedNotes++;
                if (shouldQuitLoop) break;

                GameState state = stack.Pop();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;
                    Parents[neighbor] = state;

                    if (neighbor.IsFinal())
                    {
                        finalState = neighbor;
                        shouldQuitLoop = true;
                        break;
                    }

                    else
                    {
                        stack.Push(neighbor);
                    }
                }
            }

            if (finalState is null)
            {
                throw new GameImpossibleToSolveException("Game is impossible to solve.");
            }

            this.FinalState = finalState;
        }

    }
}
