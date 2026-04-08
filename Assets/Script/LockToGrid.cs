using UnityEngine;

public class LockToGrid : MonoBehaviour
{
    public float gridSize = 1f; // Ukuran satu grid

    void Update()
    {
        SnapToGrid();
    }

    void SnapToGrid()
    {
        Vector3 pos = transform.position;

        // Snap ke grid
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        pos.z = Mathf.Round(pos.z / gridSize) * gridSize;

        transform.position = pos;
    }
}
