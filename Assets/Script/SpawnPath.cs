using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpawnPath : MonoBehaviour, IPointerClickHandler
{
    public GameObject PathObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = hit.point;
            int x = Mathf.FloorToInt(mousePos.x);
            int z = Mathf.FloorToInt(mousePos.z);

            Vector3 spawnPos = new Vector3(x + 0.5f, 2.5f, z + 0.5f);
            
            Instantiate(PathObject, spawnPos, Quaternion.identity);
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
