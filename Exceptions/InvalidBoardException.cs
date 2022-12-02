namespace ReachTheFlag.Exceptions
{
    public class InvalidBoardException: Exception
    {
        public InvalidBoardException()
        {
        }

        public InvalidBoardException(string message = "Please provide a valid format for the game board.")
            : base(message)
        {
        }

        public InvalidBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
