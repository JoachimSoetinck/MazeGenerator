using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveBacktracker : IMazeAlgorithm
{
    public IEnumerator GenerateMaze(Wall[,] maze, int width, int height, System.Action<Position, Position> drawStep, float stepDelay)
    {
        bool[,] visited = new bool[width, height];
        Stack<Position> stack = new Stack<Position>();

        Position startPos = new Position { X = Random.Range(0, width), Y = Random.Range(0, height) };
        stack.Push(startPos);
        visited[startPos.X, startPos.Y] = true;

        while (stack.Count > 0)
        {
            Position currentPos = stack.Pop();
            List<Position> neighbours = GetUnvisitedNeighbours(currentPos, visited, width, height);

            if (neighbours.Count > 0)
            {
                stack.Push(currentPos); // Push current position back to stack
                Position randomNeighbour = neighbours[Random.Range(0, neighbours.Count)];
                RemoveWallAndUpdateDictionary(currentPos, randomNeighbour, maze, drawStep);
                visited[randomNeighbour.X, randomNeighbour.Y] = true;
                stack.Push(randomNeighbour);
                yield return new WaitForSeconds(stepDelay);
            }
        }
    }

    private List<Position> GetUnvisitedNeighbours(Position p, bool[,] visited, int width, int height)
    {
        List<Position> neighbours = new List<Position>();

        if (p.X > 0 && !visited[p.X - 1, p.Y])
            neighbours.Add(new Position { X = p.X - 1, Y = p.Y });

        if (p.X < width - 1 && !visited[p.X + 1, p.Y])
            neighbours.Add(new Position { X = p.X + 1, Y = p.Y });

        if (p.Y > 0 && !visited[p.X, p.Y - 1])
            neighbours.Add(new Position { X = p.X, Y = p.Y - 1 });

        if (p.Y < height - 1 && !visited[p.X, p.Y + 1])
            neighbours.Add(new Position { X = p.X, Y = p.Y + 1 });

        return neighbours;
    }

    private void RemoveWallAndUpdateDictionary(Position currentPos, Position neighbourPos, Wall[,] maze, System.Action<Position, Position> drawStep)
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

        drawStep(currentPos, neighbourPos);
    }
}
