using ReachTheFlag.Logic;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Game
{
    public class ReachTheFlagGame
    {
        private const string MAP_FILE_PATH = "D:\\my-projects\\VersionTest\\ConsoleApp1\\map.txt";

        private GameSolver _solveStrategy;

        // For restarting the game
        private readonly GameBoard _originalBoard;
        public GameState CurrentState { get; private set; }

        public ReachTheFlagGame()
        {
            GameBoard parsedBoard = InputParser.ParseInputBoard(MAP_FILE_PATH);

            this.CurrentState = new GameState(parsedBoard);
            this._originalBoard = parsedBoard.Clone();
        }

        public GameStatus GetStatus()
        {
            if (CurrentState.IsFinal()) return GameStatus.Win;
            if (CurrentState.IsPlayerStuck()) return GameStatus.Lose;

            return GameStatus.Playing;
        }

        public bool IsFinal()
        {
            return CurrentState.IsFinal();
        }

        public void PrintBoard()
        {
            Printer.PrintBoard(this.CurrentState.Board);
        }

        public Dictionary<MoveDirection, GameState> GetAllPossibleStates()
        {
            return CurrentState.GetAllPossibleStates();
        }

        public void PrintAllPossibleStates()
        {
            var states = this.CurrentState.GetAllPossibleStates();
            foreach (KeyValuePair<MoveDirection, GameState> kvp in states)
            {
                Console.WriteLine("Direction: {0} - Final State: {1}", kvp.Key, kvp.Value.IsFinal());
                Printer.PrintBoard(kvp.Value.Board);
                Console.WriteLine("------");
            }
        }

        public void Restart()
        {
            this.CurrentState = new GameState(this._originalBoard);
        }
    }
}
