namespace ReachTheFlag.Exceptions
{
    public class CellImpossibleToReachException : Exception
    {
        public CellImpossibleToReachException()
        {
        }

        public CellImpossibleToReachException(string message = "Cell is impossible to reach. Player cannot step on it.")
            : base(message)
        {
        }

        public CellImpossibleToReachException(string message = "Cell is impossible to reach. Player cannot step on it.", Exception inner)
            : base(message, inner)
        {
        }
    }
}
