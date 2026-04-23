using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathPlacement : MonoBehaviour
{
    public GameObject objectPath;
    // private bool isClick = false;
    // private bool isDone = false;
    private bool isPlace = false;
    private bool canPlace = true;
    public static event Action OnPlayerSearch;
    private int rotationSide = 0;
    public static Func<int, bool> OnPathManage;
    public int pathValue;
    //public float x, y, z;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPath = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isPlace)
        {
            Debug.Log("Placement");
            if(!canPlace)
            {
                Debug.Log("Tdk bisa diletakkan");
                return;
            }
            bool pathCraft = OnPathManage(pathValue);
            if(!pathCraft) return; 
            isPlace = true;
            foreach(Transform child in objectPath.GetComponentsInChildren<Transform>())
            {
                if(child == transform) continue;
                child.gameObject.tag = "Obstacle";
            }

            // isClick = true;
            // isDone = true;
            GridManager.Instance.SetObstacles();
            OnPlayerSearch?.Invoke();
            
        }

        if(!isPlace && (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1)))
        {

            rotationSide += 90;
            transform.rotation = Quaternion.Euler(0, rotationSide, 0);
        }

        if(!isPlace)
        {
            canPlace = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 pos = hit.point;

                int x = Mathf.FloorToInt(pos.x);
                int z = Mathf.FloorToInt(pos.z);

                transform.position = new Vector3(x + 0.5f, 2.5f, z + 0.5f);
            }

            Transform[] child = objectPath.GetComponentsInChildren<Transform>();
            if(child == null) return;
            foreach(Transform childs in child)
            {
                if(childs == transform) continue;
                Vector3 worldPos = childs.position;
                Vector3Int gridPos = GridManager.Instance.WorldToGrid(worldPos);
                //Debug.Log("Child world: " + worldPos + " Grid: " + gridPos);

                //Vector3Int above = new Vector3Int(gridPos.x, gridPos.y + 1, gridPos.z);
                if(GridManager.Instance.grid.ContainsKey(gridPos))
                {
                    if(GridManager.Instance.grid[gridPos] == GridManager.GridType.Obstacle)
                    {
                        canPlace = false;
                        break;
                    }
                }

            }


        }
    }
}
