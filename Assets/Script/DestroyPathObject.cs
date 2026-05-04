using UnityEngine;
using UnityEngine.EventSystems;

public class DestroyPathObject : MonoBehaviour, IPointerEnterHandler
{
    public GameObject currentPath;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentPath != null)
        {
            Debug.Log("Destroyeedd");
            PathPlacement pathPlacement = currentPath.GetComponent<PathPlacement>();
            PathManager.pathManager.pathsUnit += pathPlacement.pathValue;
            PathManager.pathManager.canSpawnPath = true;
            Destroy(currentPath);
            currentPath = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnDisable()
    {
        if(this.gameObject.activeInHierarchy == false)
        {
            currentPath = null;
        }
    }

    // void OnEnable()
    // {
    //     PathPlacement.OnPlacementYet += 
    // }

    // void placementdone()
    // {
        
    // }
}
