using ReachTheFlag.Cells;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic.Solvers.GraphSolvers.AStar
{
    public abstract class AStarHeuristic
    {
        public abstract double GetCostBetweenTwoCells(BoardCell a, BoardCell b);
        public double[][] GetHeuristicArrayForBoard(GameBoard board)
        {
            BoardCell[][] cells = board.GetAllCells();
            double[][] result = new double[cells.Length][];

            for (int i = 0; i < cells.Length; i++)
            {
                result[i] = new double[cells[i].Length];

                for (var j = 0; j < cells[i].Length; j++)
                {
                    result[i][j] = GetCostBetweenTwoCells(board.GetCell(i, j), board.FlagCell);
                }
            }

            return result;
        }
    }
}
