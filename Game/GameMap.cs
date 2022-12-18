using ReachTheFlag.Structure;

namespace ReachTheFlag.Game
{
    public record class GameMap
    {
        public int Key;
        public readonly string Name;
        public readonly string FilePath;
        public readonly GameBoard Board;

        public GameMap(int key, string name, GameBoard board, string filepath)
        {
            Key = key;
            Name = name;
            Board = board;
            FilePath = filepath;
        }
    }
}
