using ReachTheFlag.Exceptions;
using ReachTheFlag.Logic.Statistics;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public abstract class GameSolver
    {
        protected GameState FinalState;
        protected readonly GameState InitialNode;

        public string Name { get; protected set; }
        protected SolvedGameStatistics Statistics;

        public GameSolver(string name, GameState node)
        {
            Name = name;
            InitialNode = node;
            Statistics = new SolvedGameStatistics();
        }

        public abstract void Solve();
        public void SolveAndPrintSolutionStatistics()
        {
            Statistics.StartTimer();

            try
            {
                Solve();
                Statistics.StopTimer();
                printStatistics();
            }

            catch (GameImpossibleToSolveException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void printStatistics()
        {
            FillStatisticsData();

            Printer.PrintBoard(FinalState.Board);
            Printer.PrintStatistics(Statistics);
        }

        protected virtual void FillStatisticsData() { }
    }
}
