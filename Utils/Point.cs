using ReachTheFlag.Utils;

namespace ReachTheFlag.Structure
{
    public class Point : ICloneable<Point>
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}
