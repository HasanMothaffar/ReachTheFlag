# Reach The Flag

This game was developed as a university project for the Intelligent Search Algorithms course (4th year, 1st semester).
This is the original game (https://www.puzzleplayground.com/reach_the_flag)

## How to play

When the project starts, please enter 1 on your keyboard to start the game.

For more info on how the original game works, check out its link here: https://www.puzzleplayground.com/reach_the_flag

The basic idea is that we have a an (n x m) grid of cells that the player should walk through in order to reach the flag.

There are various kinds of cells, the most common one being a one-step cell which means that once the player steps on the cell, he cannot
step on it again. The player has to carefully walk the grid so that he doesn't get stuck in a state from which he cannot reach the flag.

### Controls

W: Move up
D: Move right
S: Move down
A: Move left

### Symbols

*: Current player position. This symbol moves as you press movement keys.
o: A one-step cell. Once you step on this cell, you cannot step on it again.
f: Flag cell. This is the cell that you're supposed to be standing at when the game finishes.

## Purpose

The goal of this project is to demonstrate the usage of different algorithms (DFS, BFS, Uniform Cost) in a real-life scenario (in our case, a game).

When the project starts, you can select one of these ways to solve the game:

1. User play: You play the game through the keyboard (WASD controls only)
2. DFS: The computer will solve this game using the DFS graph traversal algorithm and display the result
3. BFS
4.

## Map