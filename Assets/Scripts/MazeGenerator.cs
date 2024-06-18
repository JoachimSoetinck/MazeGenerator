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
    public enum AlgorithmType { RecursiveBacktracker }
    public AlgorithmType selectedAlgorithm = AlgorithmType.RecursiveBacktracker;

    private Wall[,] maze;
    private IMazeAlgorithm mazeAlgorithm;

    private void Awake()
    {
        switch (selectedAlgorithm)
        {
            case AlgorithmType.RecursiveBacktracker:
            default:
                mazeAlgorithm = new RecursiveBacktracker();
                break;
        }
    }

    public IEnumerator GenerateMazeOverTime(int width, int height, System.Action<Position, Position> drawStep)
    {
        InitializeMaze(width, height);
        yield return StartCoroutine(mazeAlgorithm.GenerateMaze(maze, width, height, drawStep, stepDelay));
    }

    private void InitializeMaze(int width, int height)
    {
        maze = new Wall[width, height];
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
    }
}