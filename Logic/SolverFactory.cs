﻿using ReachTheFlag.Game;
using ReachTheFlag.Structure;

namespace ReachTheFlag.Logic
{
    public class SolverFactory
    {
        public static GameSolver GetSolverForGame(SolverStrategy strategy, GameState initialNode)
        {
            return strategy switch
            {
                SolverStrategy.UserInput => new UserInputSolver(initialNode),
                SolverStrategy.DFS => new DFSSolver(initialNode),
                SolverStrategy.BFS => new BFSSolver(initialNode),
                SolverStrategy.UniformCost => new UniformCostSolver(initialNode),

                _ => new UserInputSolver(initialNode)
            };
        }
    }
}
