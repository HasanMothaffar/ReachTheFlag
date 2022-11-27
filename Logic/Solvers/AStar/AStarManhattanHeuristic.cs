using ReachTheFlag.Cells;

namespace ReachTheFlag.Logic.Solvers.AStar
{
    internal class AStarManhattanHeuristic : AStarHeuristic
    {
        public override double GetCostBetweenTwoCells(BoardCell a, BoardCell b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}
