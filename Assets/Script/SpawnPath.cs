using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpawnPath : MonoBehaviour, IPointerClickHandler
{
    public GameObject PathObject;
    private PathPlacement pathPlacement;
    private int pathsValue;
    public DestroyPathObject destroyPath;
    public static event Action<bool> OnDestroyPanelShow;
    public static Func<int, bool> OnPathManage;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!PathManager.pathManager.SpawnPath) return;
        //Debug.Log("coba1");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("coba2");
            Vector3 mousePos = hit.point;
            int x = Mathf.FloorToInt(mousePos.x);
            int z = Mathf.FloorToInt(mousePos.z);

            Vector3 spawnPos = new Vector3(x + 0.5f, 2.5f, z + 0.5f);
            
            destroyPath.currentPath = Instantiate(PathObject, spawnPos, Quaternion.identity);
            OnDestroyPanelShow?.Invoke(true);
            PathManager.pathManager.SpawnPath = false;
            bool pathCraft = OnPathManage(pathsValue);
            if(!pathCraft) return; 
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pathPlacement = GetComponent<PathPlacement>();
        pathsValue = pathPlacement.pathValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
