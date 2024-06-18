using System.Collections;
using System.Collections.Generic;

public interface IMazeAlgorithm
{
    IEnumerator GenerateMaze(Wall[,] maze, int width, int height, System.Action<Position, Position> drawStep, float stepDelay);
}

