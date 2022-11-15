using ReachTheFlag.Game;

namespace ReachTheFlag.Logic
{
	public class UserInputSolver : IGameSolver
	{
		private readonly Dictionary<string, MoveDirection> _moveDirections = new()
		{
			{ "a", MoveDirection.Left },
			{ "d", MoveDirection.Right },
			{ "w", MoveDirection.Up },
			{ "s", MoveDirection.Down },
		};

        private ReachTheFlagGame _game;

        public UserInputSolver(ReachTheFlagGame game)
        {
            this._game = game;
        }

        public string Name => "User Input";

        public void Solve()
		{
            char pressedKey;
            GameStatus status;

            _game.PrintBoard();

            while ((status = _game.GetStatus()) == GameStatus.Playing)
            {
                pressedKey = Console.ReadKey(true).KeyChar;
                Console.Clear();
                respondToUserInput(pressedKey);
                _game.PrintBoard();
            }

            if (status == GameStatus.Lose) Console.WriteLine("Player is stuck.");
            else if (status == GameStatus.Win) Console.WriteLine("You won!");

            _game.PrintPlayerPath();
		}

		private void respondToUserInput(char pressedKey)
		{
            string lowerCaseKey = pressedKey.ToString().ToLower();

            if (this._moveDirections.ContainsKey(lowerCaseKey))
            {
                MoveDirection direction = this._moveDirections[lowerCaseKey];
                Console.WriteLine($"Move direction: {direction}");
                _game.CurrentState.ShiftPlayerPosition(direction);
            }
        }
	}
}
