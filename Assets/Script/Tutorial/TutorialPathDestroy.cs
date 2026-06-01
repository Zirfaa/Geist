using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPathDestroy : MonoBehaviour, IPointerEnterHandler
{
    public GameObject currentPath;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentPath != null && TutorialManager.TM.currentStep == 4)
        {
            TutorialManager.TM.NextStep();
            Debug.Log("Destroyeedd");
            TutorialPathPlacement pathPlacement = currentPath.GetComponent<TutorialPathPlacement>();
            TutorialPathManager.TPM.pathsUnit += pathPlacement.pathValue;
            TutorialPathManager.TPM.canSpawnPath = true;
            Destroy(currentPath);
            currentPath = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnDisable()
    {
        if(this.gameObject.activeInHierarchy == false)
        {
            currentPath = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
