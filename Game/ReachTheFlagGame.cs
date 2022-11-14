using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Game
{
    public class ReachTheFlagGame
    {
        private const string MAP_FILE_PATH = "D:\\my-projects\\VersionTest\\ConsoleApp1\\map.txt";

        private GameState _currentState;

        // For restarting the game
        private readonly GameBoard _originalBoard;

        private readonly Dictionary<string, MoveDirection> moveDirections = new()
        {
            { "a", MoveDirection.Left },
            { "d", MoveDirection.Right },
            { "w", MoveDirection.Up },
            { "s", MoveDirection.Down },
        };

        public ReachTheFlagGame()
        {
            GameBoard parsedBoard = InputParser.ParseInputBoard(MAP_FILE_PATH);

            this._currentState = new GameState(parsedBoard);

            this._originalBoard = parsedBoard.Clone();
        }

        public GameStatus GetStatus()
        {
            if (_currentState.IsFinal()) return GameStatus.Win;
            if (_currentState.IsPlayerStuck()) return GameStatus.Lose;

            return GameStatus.Playing;
        }

        public bool IsFinal()
        {
            return _currentState.IsFinal();
        }

        public void RespondToUserInput(char pressedKey)
        {
            string lowerCaseKey = pressedKey.ToString().ToLower();

            if (this.moveDirections.ContainsKey(lowerCaseKey))
            {
                MoveDirection direction = this.moveDirections[lowerCaseKey];
                Console.WriteLine($"Move direction: {direction}");
                this._currentState.ShiftPlayerPosition(direction);
            }
        }

        public void PrintBoard()
        {
            Printer.PrintBoard(this._currentState.Board);
        }

        public GameState GetCurrentState()
        {
            return _currentState.Clone();
        }

        public Dictionary<MoveDirection, GameState> GetAllPossibleStates()
        {
            return _currentState.GetAllPossibleStates();
        }

        public void PrintAllPossibleStates()
        {
            var states = this._currentState.GetAllPossibleStates();
            foreach (KeyValuePair<MoveDirection, GameState> kvp in states)
            {
                Console.WriteLine("Direction: {0} - Final State: {1}", kvp.Key, kvp.Value.IsFinal());
                Printer.PrintBoard(kvp.Value.Board);
                Console.WriteLine("------");
            }
        }

        public void Restart()
        {
            this._currentState = new GameState(this._originalBoard);
        }
    }
}
