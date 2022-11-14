using ReachTheFlag.Game;
using ReachTheFlag.Structure;

ReachTheFlagGame game = new ReachTheFlagGame(new Point(0, 0));

//Solver solver = new DFS();
//solver.SolveGame(game);

GameStatus status;

game.PrintBoard();

while ((status = game.GetStatus()) == GameStatus.Playing)
{
    char pressedKey = Console.ReadKey(true).KeyChar;

    if (pressedKey == 'r' || pressedKey == 'R')
    {
        game.Restart();
    }

    Console.Clear();
    game.Update(pressedKey);
    game.PrintBoard();

    //Console.WriteLine("\nPossible States:\n");
    //game.PrintAllPossibleStates();
}

if (status == GameStatus.Lose) Console.WriteLine("Player is stuck.");
else if (status == GameStatus.Win) Console.WriteLine("You won!");