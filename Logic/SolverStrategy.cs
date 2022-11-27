using System.ComponentModel;

namespace ReachTheFlag.Logic
{
    public enum SolverStrategy
    {
        [Description("User Input")]
        UserInput = 1,

        [Description("DFS")]
        DFS = 2,

        [Description("BFS")]
        BFS = 3,

        [Description("Uniform Cost")]
        UniformCost = 4,

        [Description("A* (A Star)")]
        AStar = 5
    }
}
