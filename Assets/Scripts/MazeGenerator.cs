using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall
{
    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;
    public bool Visited = false;
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public Wall SharedWall;
}

public class MazeGenerator : MonoBehaviour
{
    public float stepDelay = 0.1f;

    public IEnumerator GenerateMazeOverTime(int width, int height, Wall[,] maze, System.Action<Position, Position> drawStep)
    {
        // Start position for maze generation
        Position startPos = new Position { X = Random.Range(0, width), Y = Random.Range(0, height) };
        maze[startPos.X, startPos.Y].Visited = true;

        // Start recursive maze generation
        yield return StartCoroutine(RecursiveBacktracker(maze, startPos, width, height, drawStep));
    }

    private IEnumerator RecursiveBacktracker(Wall[,] maze, Position currentPos, int width, int height, System.Action<Position, Position> drawStep)
    {
        // Get unvisited neighbours of current position
        List<Neighbour> unvisitedNeighbours = GetUnvisitedNeighbours(currentPos, maze, width, height);

        // Continue until there are unvisited neighbours
        while (unvisitedNeighbours.Count > 0)
        {
            // Randomly select a neighbour
            int randomIndex = Random.Range(0, unvisitedNeighbours.Count);
            Position neighbourPos = unvisitedNeighbours[randomIndex].Position;

            // Update walls between current position and selected neighbour
            UpdateWalls(currentPos, neighbourPos, maze);

            // Mark neighbour as visited
            maze[neighbourPos.X, neighbourPos.Y].Visited = true;

            // Execute draw step action
            drawStep(currentPos, neighbourPos);
            yield return new WaitForSeconds(stepDelay);

            // Recursively call backtracker for the selected neighbour
            yield return StartCoroutine(RecursiveBacktracker(maze, neighbourPos, width, height, drawStep));

            // Update unvisited neighbours for current position
            unvisitedNeighbours = GetUnvisitedNeighbours(currentPos, maze, width, height);
        }
    }

    private void UpdateWalls(Position currentPos, Position neighbourPos, Wall[,] maze)
    {
        if (neighbourPos.X == currentPos.X)
        {
            if (neighbourPos.Y > currentPos.Y)
            {
                maze[currentPos.X, currentPos.Y].Up = false;
                maze[neighbourPos.X, neighbourPos.Y].Down = false;
            }
            else
            {
                maze[currentPos.X, currentPos.Y].Down = false;
                maze[neighbourPos.X, neighbourPos.Y].Up = false;
            }
        }
        else
        {
            if (neighbourPos.X > currentPos.X)
            {
                maze[currentPos.X, currentPos.Y].Right = false;
                maze[neighbourPos.X, neighbourPos.Y].Left = false;
            }
            else
            {
                maze[currentPos.X, currentPos.Y].Left = false;
                maze[neighbourPos.X, neighbourPos.Y].Right = false;
            }
        }
    }

    private List<Neighbour> GetUnvisitedNeighbours(Position p, Wall[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0 && !maze[p.X - 1, p.Y].Visited)
            list.Add(new Neighbour { Position = new Position { X = p.X - 1, Y = p.Y }, SharedWall = maze[p.X, p.Y] });

        if (p.X < width - 1 && !maze[p.X + 1, p.Y].Visited)
            list.Add(new Neighbour { Position = new Position { X = p.X + 1, Y = p.Y }, SharedWall = maze[p.X, p.Y] });

        if (p.Y > 0 && !maze[p.X, p.Y - 1].Visited)
            list.Add(new Neighbour { Position = new Position { X = p.X, Y = p.Y - 1 }, SharedWall = maze[p.X, p.Y] });

        if (p.Y < height - 1 && !maze[p.X, p.Y + 1].Visited)
            list.Add(new Neighbour { Position = new Position { X = p.X, Y = p.Y + 1 }, SharedWall = maze[p.X, p.Y] });

        return list;
    }
}
