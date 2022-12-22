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

        [Description("A*")]
        AStar = 5,

        [Description("Uniform Cost (path finding)")]
        UniformCostPathFinding = 6,

        [Description("A* (path finding)")]
        AStarPathFinding = 7
    }
}
