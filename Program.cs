using ReachTheFlag.Game;

ReachTheFlagGame game = new ReachTheFlagGame();

//Solver solver = new DFS();
//solver.SolveGame(game);


game.PrintBoard();

char pressedKey;
GameStatus status;

while ((status = game.GetStatus()) == GameStatus.Playing)
{
    pressedKey = Console.ReadKey(true).KeyChar;
    Console.Clear();
    game.RespondToUserInput(pressedKey);
    game.PrintBoard();

    Console.WriteLine("\nPossible States:\n");
    game.PrintAllPossibleStates();
}

if (status == GameStatus.Lose) Console.WriteLine("Player is stuck.");
else if (status == GameStatus.Win) Console.WriteLine("You won!");