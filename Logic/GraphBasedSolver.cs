using ReachTheFlag.Cells;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public abstract class GraphBasedSolver : GameSolver
    {
        protected Dictionary<BoardCell, BoardCell?> Parents = new();

        protected GraphBasedSolver(string name, GameState node) : base(name, node)
        {
            Parents[this.InitialNode.PlayerCell] = null;
        }

        protected override void FillStatisticsData()
        {
            Stack<BoardCell> stack = new();
            BoardCell? c = Parents[Statistics.FinalState.PlayerCell];

            while (c is not null)
            {
                stack.Push(c);
                c = Parents[c];
                this.Statistics.SolutionDepth++;
            }

            while (stack.Count > 0)
            {
                this.Statistics.PlayerPath.AddCell(stack.Pop());
            }
        }
    }
}
