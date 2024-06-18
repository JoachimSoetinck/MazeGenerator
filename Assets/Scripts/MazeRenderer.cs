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

    void Start()
    {
        StartCoroutine(GenerateAndDrawMaze());
    }

    private IEnumerator GenerateAndDrawMaze()
    {
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width * size, 1, height * size);

        RenderAllWalls();
        yield return StartCoroutine(mazeGenerator.GenerateMazeOverTime(width, height, RemoveWall));
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
                    RenderWall(position + new Vector3(0, 0, -size / 2), Quaternion.identity);

                if (i == width - 1)
                    RenderWall(position + new Vector3(size / 2, 0, 0), Quaternion.Euler(0, 90, 0));

                RenderWall(position + new Vector3(-size / 2, 0, 0), Quaternion.Euler(0, 90, 0));
                RenderWall(position + new Vector3(0, 0, size / 2), Quaternion.identity);
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
            if (neighbourPos.Y > j)
                RemoveWallAndUpdateDictionary(position + new Vector3(0, 0, size / 2));
            else
                RemoveWallAndUpdateDictionary(position + new Vector3(0, 0, -size / 2));
        }
        else
        {
            if (neighbourPos.X > i)
                RemoveWallAndUpdateDictionary(position + new Vector3(size / 2, 0, 0));
            else
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
