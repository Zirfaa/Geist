using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialSpawnPath : MonoBehaviour, IPointerClickHandler
{
    public GameObject PathObject;
    private TutorialPathPlacement tutorialPathPlacement;
    private int pathsValue;
    public TutorialPathDestroy destroyPath;
    public static event Action<bool> OnDestroyPanelShow;
    public static Func<int, bool> OnPathManage;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!TutorialPathManager.TPM.canSpawnPath && (TutorialManager.TM.currentStep != 1 && TutorialManager.TM.currentStep != 3)) return;
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
            
            bool pathCraft = OnPathManage?.Invoke(pathsValue) ?? false;
            if(!pathCraft) return; 
            
            destroyPath.currentPath = Instantiate(PathObject, spawnPos, Quaternion.identity);
            TutorialManager.TM.NextStep();
            OnDestroyPanelShow?.Invoke(true);
            TutorialPathManager.TPM.canSpawnPath = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        tutorialPathPlacement = PathObject.GetComponent<TutorialPathPlacement>();
        pathsValue = tutorialPathPlacement.pathValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
