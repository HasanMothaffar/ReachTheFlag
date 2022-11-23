using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    internal class UniformCostSolver : IGameSolver
    {
        public string Name => "Uniform Cost";
        private ReachTheFlagGame _game;

        public UniformCostSolver(ReachTheFlagGame game)
        {
            _game = game;
        }

        private int[][] getDistancesArray(BoardCell[][] cells)
        {
            int rowsCount = cells.Length;
            int[][] dist = new int[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
            {
                int columnsCount = cells[i].Length;
                dist[i] = new int[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    dist[i][j] = cells[i][j].IsPlayerVisiting ? 0 : int.MaxValue;
                }
            }

            return dist;
        }

        private BoardCell[][] getParentsArray(BoardCell[][] cells)
        {
            BoardCell[][] parents = new BoardCell[cells.Length][];
            for (int i = 0; i < cells.Length; i++)
            {
                parents[i] = new BoardCell[cells[i].Length];
                for (int j = 0; j < parents[i].Length; j++)
                {
                    parents[i][j] = null;
                }
            }

            return parents;
        }

        public void Solve()
        {
            GameState initialState = _game.CurrentState;
            BoardCell[][] cells = initialState.Board.GetAllCells();

            // Dijkstra Data structures
            PriorityQueue<GameState, int> queue = new();
            int[][] dist = getDistancesArray(cells);

            // For printing the path
            BoardCell[][] cellParents = getParentsArray(cells);

            queue.Enqueue(initialState, initialState.Weight);

            while (queue.Count > 0)
            {
                GameState state = queue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;
                    int possibleShortestDistance = dist[state.X][state.Y] + neighbor.Weight;

                    if (possibleShortestDistance < dist[neighbor.X][neighbor.Y])
                    {
                        queue.Enqueue(neighbor, possibleShortestDistance);
                        dist[neighbor.X][neighbor.Y] = possibleShortestDistance;
                        cellParents[neighbor.X][neighbor.Y] = _game.CurrentState.Board.GetCell(state.X, state.Y);
                    }
                }
            }

            printShortestPath(cellParents);
            printShortestPathCost(dist);
        }

        private void printShortestPath(BoardCell[][] cellParents)
        {
            var flagCell = _game.CurrentState.Board.FlagCell;

            Console.WriteLine("\nPlayer path: \n");
            BoardCell cell = cellParents[flagCell.X][flagCell.Y];

            // Print parents of flag cell in reverse order
            while (cell is not null)
            {
                Console.WriteLine(cell);
                cell = cellParents[cell.X][cell.Y];
            }

            Console.WriteLine();
        }

        private void printShortestPathCost(int[][] dist)
        {
            var flagCell = _game.CurrentState.Board.FlagCell;
            Console.WriteLine($"Shortest path cost: {dist[flagCell.X][flagCell.Y]}");
            Console.WriteLine("All distances from player:\n");

            foreach (var row in dist)
            {
                foreach (var entry in row)
                {
                    Console.Write($"{entry} ");
                }

                Console.WriteLine();
            }
        }
    }
}
