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
            Destroy(currentPath);
            currentPath = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
