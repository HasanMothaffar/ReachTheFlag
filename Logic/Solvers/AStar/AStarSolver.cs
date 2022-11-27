﻿using ReachTheFlag.Cells;
using ReachTheFlag.Game;
using ReachTheFlag.Logic.Solvers.AStar;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    internal class AStarSolver : GraphBasedSolver
    {
        private readonly int[][] _dist;
        private readonly double[][] _heuristicValues;

        private readonly BoardCell[][] _cells;

        public AStarSolver(GameState initialNode) : base("A* (star)", initialNode)
        {
            this._cells = initialNode.Board.GetAllCells();
            this._dist = getDistancesArray();

            AStarHeuristic heuristic = new AStarManhattanHeuristic();
            this._heuristicValues = heuristic.GetHeuristicArrayForBoard(initialNode.Board);
        }

        private int[][] getDistancesArray()
        {
            int rowsCount = _cells.Length;
            int[][] dist = new int[rowsCount][];

            for (int i = 0; i < rowsCount; i++)
            {
                int columnsCount = _cells[i].Length;
                dist[i] = new int[columnsCount];

                for (var j = 0; j < columnsCount; j++)
                {
                    dist[i][j] = _cells[i][j].IsPlayerVisiting ? 0 : int.MaxValue;
                }
            }

            return dist;
        }
        public override void Solve()
        {
            GameState? finalState = null;

            PriorityQueue<GameState, double> queue = new();
            queue.Enqueue(this.InitialNode, 0);

            while (queue.Count > 0)
            {
                GameState currentState = queue.Dequeue();

                foreach (KeyValuePair<MoveDirection, GameState> kvp in currentState.GetAllNeighboringStates())
                {
                    GameState neighbor = kvp.Value;

                    int possibleShortestDistance = _dist[currentState.X][currentState.Y] + neighbor.Weight;

                    if (possibleShortestDistance < _dist[neighbor.X][neighbor.Y])
                    {
                        _dist[neighbor.X][neighbor.Y] = possibleShortestDistance;

                        double priority = possibleShortestDistance + _heuristicValues[neighbor.X][neighbor.Y];
                        queue.Enqueue(neighbor, priority);

                        Parents[neighbor] = currentState;

                        if (neighbor.PlayerCell.IsFlag)
                        {
                            finalState = neighbor;
                        }
                    }
                }
            }

            this.FinalState = finalState;
        }

        private void printShortestPathCost()
        {
            var flagCell = this.InitialNode.Board.FlagCell;
            Console.WriteLine($"Shortest path cost: {_dist[flagCell.X][flagCell.Y]}");
            Console.WriteLine("All distances from player:\n");

            foreach (var row in _dist)
            {
                foreach (var entry in row)
                {
                    Console.Write($"{entry} ");
                }

                Console.WriteLine();
            }
        }

        protected override void PrintSpecificStatistics()
        {
            base.PrintSpecificStatistics();
            printShortestPathCost();
        }
    }
}