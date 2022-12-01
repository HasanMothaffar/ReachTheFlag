using ReachTheFlag.Exceptions;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;
using System.Diagnostics;

namespace ReachTheFlag.Logic
{
    public abstract class GameSolver
    {
        private readonly Stopwatch _solutionWatch;
        protected readonly PlayerPath PlayerPath;
        protected GameState FinalState;

        public string Name { get; protected set; }

        protected readonly GameState InitialNode;

        public GameSolver(string name, GameState node)
        {
            Name = name;
            InitialNode = node;
            _solutionWatch = Stopwatch.StartNew();
            PlayerPath = new PlayerPath();
        }

        public abstract void Solve();
        public void SolveAndPrintSolutionStatistics()
        {
            _solutionWatch.Start();

            try
            {
                this.Solve();
                _solutionWatch.Stop();
                printStatistics();
            }

            catch (GameImpossibleToSolveException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void printStatistics()
        {
            Printer.PrintBoard(FinalState.Board);
            Console.WriteLine("Elapsed time: {0}s", _solutionWatch.Elapsed.TotalSeconds);

            var visitedCells = PlayerPath.GetCells();
            if (visitedCells.Count > 0)
            {
                Console.WriteLine("Player path: ");
                visitedCells.ForEach(cell => Console.WriteLine(cell));
            }

            PrintSpecificStatistics();
        }

        protected virtual void PrintSpecificStatistics() { }
    }
}
