# Reach The Flag

This game was developed as a university project for the Intelligent Search Algorithms course (4th year, 1st semester).
This is the original game (https://www.puzzleplayground.com/reach_the_flag)

## Goal of The Game

The basic idea is that we have a an (n x m) grid of cells that you should walk through in order to reach the flag.

Some cells allow you to step on them only once and will disappear afterwards, while others allow multiple steps. Your goal
is to make all of the cells disappear in a way that your final position winds up at the flag cell.

You have to carefully walk the grid so that you don't get stuck in a state from which you cannot reach the flag.

## How To Play

When the game starts, you can select how you want to solve it:

-   Select **1** if you want to play the game yourself using the keyboard and the controls below
-   Select any other number to let the computer solve the game. Beside each number is the name of the algorithm that the computer will use

### Keyboard Controls

W: Move up

D: Move right

S: Move down

A: Move left

### Symbols

`*`: Current player position. This symbol moves as you press movement keys.

`1`: A cell that allows stepping on it only one time. Once the player steps on it, it will turn to 0.

`2`: A cell that allows stepping on it only twice. You get the idea, there might be 3, 4, 5, .etc.

`g`: Gap cell. A cell that the player cannot step on.

`f`: Flag cell. This is the cell that you're supposed to be standing on when the game finishes.

`p`: Initial player position. This is where you start the game.

## Algorithms

The goal of this project is to demonstrate the usage of different algorithms (DFS, BFS, Uniform Cost) in a real-life scenario (in our case, a game).

Here's a list of the supported algorithms:

1. User play: You play the game through the keyboard (WASD controls only)
2. DFS: The computer will solve this game using the DFS graph traversal algorithm and display the result
3. BFS: The computer will solve this game using the BFS graph traversal algorithm and display the result
4. Uniform Cost (Dijkstra): The computer will find the shortest path between the initial player position and flag cell based on constant weights for each cell.
5. A Star (A\*): The computer will find the shortest path between the initial player position and flag cell based on a heuristic cost function.

## Map

The game reads its map from a text file that is specified in `program.cs`. You can provide your own map file when instantiating the game object:

`string mapFilePath = "map.txt"; ReachTheFlagGame game = new ReachTheFlagGame(mapFilePath);`

### Map Format

This game requires a special format for map files so it can parse them. The map file should consist of (n) lines \* (m) columns.

Each line represents a row in the game, and it follows this format:

CellType NumberOfAllowedSteps Weight (for path algorithms)

**Examples**:

-   `p11`: A player cell that you can step on only once and has weight 1.
-   `n23`: A normal cell that you can step on twice and has weight 3.
-   `g`: A gap cell that you can't step on.
-   `f3`: Flag cell that has weight 3.

**A complete example**:

```
p11,n13,f3
g,n21,n24
```

First row: `p11,n13,f3`

| Cell Type | Allowed Number Of Moves | Weight |
| --------- | ----------------------- | ------ |
| Player    | 1                       | 1      |
| Normal    | 1                       | 1      |
| Flag      | Infinite                | 3      |

Second row: `g,n21,n24`

| Cell Type | Allowed Number Of Moves | Weight |
| --------- | ----------------------- | ------ |
| Gap       | 0                       | 0      |
| Normal    | 2                       | 1      |
| Normal    | 2                       | 4      |

### Cell Types

-   `n`: Normal cell.
-   `p`: Player cell.
-   `g`: Gap cell.
-   `f`: Flag cell.

**IMPORTANT NOTE:**

-   Do not provide allowed number of steps to flag cells. "f11" is wrong. "f1" is right. That's because flag cells have an infinite number of allowed steps.
-   Do not provide neither allowed number of steps or weight to gap cells.
-   Provide only one flag cell and one player cell.
-   Cells in the map file should be separated by commas **WITHOUT SPACES!**
-   Rows in the map are separated by a newline character.

## TODO

-   Replace Board.GetAllCells() with iterator pattern
-   Make all map grid n \* m (no jagged arrays) by providing gap cells in the place of empty cells
