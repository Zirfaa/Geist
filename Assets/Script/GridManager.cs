using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public enum GridType
    {
        Empety,
        Path,
        Obstacle
    }
    public Dictionary<Vector3Int, GridType> grid = new Dictionary<Vector3Int, GridType>();
    public int width = 10;
    public int height = 1;
    public int depth = 10;

    void Awake()
    {
        Instance = this;
        CreateGrid();
        SetObstacles();
    }

    void Start()
    {
        
    }

    void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Vector3Int pos = new Vector3Int(
                        (int)transform.position.x + x,
                        (int)transform.position.y + y,
                        (int)transform.position.z + z
                    );

                    grid[pos] = GridType.Empety;
                }
            }
        }
    }

    public void SetObstacles()
    {
        List<Vector3Int> keys = new List<Vector3Int>(grid.Keys);

        foreach(var key in keys)
        {
            grid[key] = GridType.Empety;
        }

        foreach (var cell in keys)
        {
            Vector3 worldPos = GridToWorld(cell);

            Collider[] hits = Physics.OverlapBox(worldPos, Vector3.one * 0.4f);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Obstacle"))
                {
                    grid[cell] = GridType.Obstacle;
                    break;
                }
            }
        }

        SetGridAir();
    }

    public void SetGridAir()
    {
        Dictionary<Vector3Int, GridType> newGrid = new Dictionary<Vector3Int, GridType>(grid);

        foreach (var tile in grid)
        {
            Vector3Int pos = tile.Key;

            // kalau obstacle skip
            if (grid[pos] == GridType.Obstacle)
                continue;

            Vector3Int below = new Vector3Int(pos.x, pos.y - 1, pos.z);

            if (!grid.ContainsKey(below))
            {
                newGrid[pos] = GridType.Empety;
                continue;
            }

            // kalau bawahnya bukan obstacle → melayang
            if (grid[below] == GridType.Path)
            {
                newGrid[pos] = GridType.Empety;
            }
            else if(grid[below] == GridType.Obstacle)
            {
                newGrid[pos] = GridType.Path;
            }
        }

        grid = newGrid;
    }

    public Vector3Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x);
        int y = Mathf.FloorToInt(worldPos.y);
        int z = Mathf.FloorToInt(worldPos.z);

        return new Vector3Int(x, y, z);
    }

    public Vector3 GridToWorld(Vector3Int gridPos)
    {
        return new Vector3(gridPos.x + 0.5f, gridPos.y + 0.5f, gridPos.z + 0.5f);
    }

    // public bool IsWalkable(Vector3 worldPos)
    // {
    //     Vector3Int gridPos = WorldToGrid(worldPos);

    //     if (!grid.ContainsKey(gridPos))
    //         return false;

    //     return grid[gridPos] == false;
    // }

    void OnDrawGizmos()
    {
        if (grid == null) return;

        foreach (var kvp in grid)
        {
            Vector3Int pos = kvp.Key;
            GridType occupied = kvp.Value;
            
            switch(occupied)
            {
                case GridType.Empety :
                    Gizmos.color = Color.yellow;
                    break;
                case GridType.Path :
                    Gizmos.color = Color.green;
                    break;
                case GridType.Obstacle :
                    Gizmos.color = Color.red;
                    break;
            }
            Gizmos.DrawWireCube(pos + Vector3.one * 0.5f, Vector3.one);
        }
    }
}