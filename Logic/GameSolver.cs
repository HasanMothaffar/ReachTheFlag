using ReachTheFlag.Exceptions;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public abstract class GameSolver
    {
        public string Name { get; protected set; }
        protected GameState InitialNode;

        public GameSolver(string name, GameState node)
        {
            Name = name;
            InitialNode = node;
        }

        public abstract void Solve();
        public System.Diagnostics.Stopwatch SolveAndGetElapsedTime()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                this.Solve();
                watch.Stop();
            }

            catch (GameImpossibleToSolveException e)
            {
                Console.WriteLine(e.Message);
            }

            return watch;
        }
    }
}
