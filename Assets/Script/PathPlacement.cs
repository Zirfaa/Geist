using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PathPlacement : MonoBehaviour
{
    [HideInInspector] public GameObject objectPath;
    // private bool isClick = false;
    // private bool isDone = false;
    private bool isPlace = false;
    private bool canPlace = true;
    public static event Action OnPlayerSearch;
    private int rotationSide = 0;
    public int pathValue;
    public static event Action<bool> OnDestroyPanelHide;
    public AudioClip RotatePathSound;
    public AudioClip PlacementPathSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectPath = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isPlace && !PathManager.pathManager.canSpawnPath)
        {
            //Debug.Log("Placement");
            if(!canPlace)
            {
                Debug.Log("Tdk bisa diletakkan");
                return;
            }
            
            OnDestroyPanelHide?.Invoke(false);
            isPlace = true;
            AudioManager.audioManager.PlaySFX(PlacementPathSound);
            PathManager.pathManager.canSpawnPath = true;
            Transform[] childs = objectPath.GetComponentsInChildren<Transform>();
            foreach(Transform child in childs)
            {
                if(child == transform) continue;
                child.gameObject.tag = "Obstacle";
            }
            foreach(Transform childsObjt in childs)
            {
                if(childsObjt == transform) continue;

                MeshRenderer mr = childsObjt.GetComponentInChildren<MeshRenderer>();
                if(mr != null)
                {
                    Material mats = mr.material;
                    mats.SetFloat("_OutlineSize", 0f);
                }
            }
            GridManager.Instance.SetObstacles();
            OnPlayerSearch?.Invoke();
            
        }

        if(!isPlace && (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1)))
        {
            AudioManager.audioManager.PlaySFX(RotatePathSound);
            rotationSide += 90;
            transform.rotation = Quaternion.Euler(0, rotationSide, 0);
        }

        if(!isPlace)
        {
            canPlace = true;
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;

            if(plane.Raycast(ray, out distance))
            {
                Vector3 pos = ray.GetPoint(distance);

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
              
                if(GridManager.Instance.grid.ContainsKey(gridPos))
                {
                    if(GridManager.Instance.grid[gridPos] == GridManager.GridType.Obstacle)
                    {
                        
                        canPlace = false;
                        break;
                    }
                }

                if(!GridManager.Instance.grid.ContainsKey(gridPos))
                {
                    canPlace = false;
                    break;
                }
            }

            foreach(Transform childs in child)
            {
                if(childs == transform) continue;

                MeshRenderer mr = childs.GetComponentInChildren<MeshRenderer>();
                if(mr != null)
                {
                    Material mats = mr.material;
                    mats.SetColor("_OutlineColor", canPlace ? Color.green : Color.red);
                }
            }

        }
    }
}
