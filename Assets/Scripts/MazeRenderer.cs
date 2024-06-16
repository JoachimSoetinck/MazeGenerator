using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int width = 10;

    [SerializeField]
    [Range(1, 50)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    private MazeGenerator mazeGenerator = null;

    private Dictionary<Vector3, Transform> wallObjects = new Dictionary<Vector3, Transform>();
    private Wall[,] maze;

    void Start()
    {
        InitializeMaze();
        StartCoroutine(GenerateAndDrawMaze());
    }

    private void InitializeMaze()
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

    private IEnumerator GenerateAndDrawMaze()
    {
        // Instantiate floor
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width * size, 1, height * size);

        // Render all walls
        RenderAllWalls();

        // Start maze generation and drawing
        yield return StartCoroutine(mazeGenerator.GenerateMazeOverTime(width, height, maze, RemoveWall));
    }

    private void RenderWall(Vector3 position, Quaternion rotation)
    {
        var wall = Instantiate(wallPrefab, position, rotation, transform);
        wall.localScale = new Vector3(size, wall.localScale.y, wall.localScale.z);
        wallObjects[position] = wall;
    }

    private void RenderAllWalls()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var position = new Vector3(i * size, 0, j * size);

                if (j == 0)
                    RenderWall(position + new Vector3(0, 0, -size / 2), Quaternion.identity); // Bottom wall

                if (i == width - 1)
                    RenderWall(position + new Vector3(size / 2, 0, 0), Quaternion.Euler(0, 90, 0)); // Right wall

                RenderWall(position + new Vector3(-size / 2, 0, 0), Quaternion.Euler(0, 90, 0)); // Left wall

                RenderWall(position + new Vector3(0, 0, size / 2), Quaternion.identity); // Top wall
            }
        }
    }

    private void RemoveWall(Position currentPos, Position neighbourPos)
    {
        int i = currentPos.X;
        int j = currentPos.Y;
        var position = new Vector3(i * size, 0, j * size);

        if (neighbourPos.X == i)
        {
            if (neighbourPos.Y > j) // Up
                RemoveWallAndUpdateDictionary(position + new Vector3(0, 0, size / 2));

            else // Down
                RemoveWallAndUpdateDictionary(position + new Vector3(0, 0, -size / 2));
        }
        else
        {
            if (neighbourPos.X > i) // Right
                RemoveWallAndUpdateDictionary(position + new Vector3(size / 2, 0, 0));

            else // Left
                RemoveWallAndUpdateDictionary(position + new Vector3(-size / 2, 0, 0));
        }
    }

    private void RemoveWallAndUpdateDictionary(Vector3 wallPosition)
    {
        if (wallObjects.TryGetValue(wallPosition, out Transform wallTransform))
        {
            Destroy(wallTransform.gameObject);
            wallObjects.Remove(wallPosition);
        }
    }
}
