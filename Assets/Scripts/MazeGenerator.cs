using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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

public static class MazeGenerator
{

    private static Wall[,] ApplyRecursiveBacktracker(Wall[,] maze, int width, int height)
    {

        // Choose a random starting cell
        Position startPos = new Position { X = UnityEngine.Random.Range(0, width), Y = UnityEngine.Random.Range(0, height) };

        // Mark the starting cell as visited
        maze[startPos.X, startPos.Y].Visited = true;

        // Recursively visit unvisited neighbours
        RecursiveBacktracker(maze, startPos, width, height);

        return maze;

    }

    private static void RecursiveBacktracker(Wall[,] maze, Position currentPos, int width, int height)
    {
        // Get all unvisited neighbours of the current cell
        List<Neighbour> unvisitedNeighbours = GetUnvisitedNeighbours(currentPos, maze, width, height);

        while (unvisitedNeighbours.Count > 0)
        {
            // Choose a random unvisited neighbour
            int randomIndex = UnityEngine.Random.Range(0, unvisitedNeighbours.Count);
            Position neighbourPos = unvisitedNeighbours[randomIndex].Position;

            // Remove the wall between the current cell and the chosen neighbour
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

            // Recursively visit the chosen neighbour
            maze[neighbourPos.X, neighbourPos.Y].Visited = true;
            RecursiveBacktracker(maze, neighbourPos, width, height);

            // Get the updated list of unvisited neighbours
            unvisitedNeighbours = GetUnvisitedNeighbours(currentPos, maze, width, height);
        }
    }

    private static List<Neighbour> GetUnvisitedNeighbours(Position p, Wall[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (p.X > 0 && !maze[p.X - 1, p.Y].Visited) // Left neighbour
            list.Add(new Neighbour { Position = new Position { X = p.X - 1, Y = p.Y }, SharedWall = maze[p.X, p.Y] });

        if (p.X < width - 1 && !maze[p.X + 1, p.Y].Visited) // Right neighbour
            list.Add(new Neighbour { Position = new Position { X = p.X + 1, Y = p.Y }, SharedWall = maze[p.X, p.Y] });

        if (p.Y > 0 && !maze[p.X, p.Y - 1].Visited) // Down neighbour
            list.Add(new Neighbour { Position = new Position { X = p.X, Y = p.Y - 1 }, SharedWall = maze[p.X, p.Y] });

        if (p.Y < height - 1 && !maze[p.X, p.Y + 1].Visited) // Up neighbour
            list.Add(new Neighbour { Position = new Position { X = p.X, Y = p.Y + 1 }, SharedWall = maze[p.X, p.Y] });

        return list;
    }


    public static Wall[,] Generate(int width, int height)
    {
        Wall[,] maze = new Wall[width, height];
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i, j] = new Wall();
                maze[i, j].Left = true;
                maze[i, j].Right = true;
                maze[i, j].Up = true;
                maze[i, j].Down = true;
            }
        }

        return ApplyRecursiveBacktracker(maze, width,height);
    }
}
