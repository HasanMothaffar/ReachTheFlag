using ReachTheFlag.Game;
using ReachTheFlag.Structure;
using ReachTheFlag.Utils;

namespace ReachTheFlag.Logic
{
    public class DFS : Solver
    {

        public void SolveGame(ReachTheFlagGame game)
        {
            if (game.IsFinal())
            {
                Console.WriteLine("Game is already solved.");
            }

            else
            {
                GameState initialState = game.GetCurrentState();
                SolveGame(initialState);
            }
        }

        private void SolveGame(GameState state)
        {
            foreach (KeyValuePair<MoveDirection, GameState> kvp in state.GetAllPossibleStates())
            {
                GameState possibleState = kvp.Value;

                if (possibleState.IsFinal())
                {
                    Console.WriteLine("Mabrook game done.");
                    Printer.PrintBoard(possibleState.Board);

                    return;
                }

                else
                {
                    SolveGame(possibleState);
                }
            }
        }
    }
}
