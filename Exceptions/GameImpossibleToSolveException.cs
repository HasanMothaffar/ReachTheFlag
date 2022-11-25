namespace ReachTheFlag.Exceptions
{
    public class GameImpossibleToSolveException: Exception
    {
        public GameImpossibleToSolveException()
        {
        }

        public GameImpossibleToSolveException(string message)
            : base(message)
        {
        }

        public GameImpossibleToSolveException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
