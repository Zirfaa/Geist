using UnityEngine;

public class TutorialPathManager : MonoBehaviour
{
    public static TutorialPathManager TPM;
    public int maxPaths;
    [HideInInspector] public int pathsUnit;
    public bool canSpawnPath = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathsUnit = maxPaths;
    }
    void Awake()
    {
        if(TPM == null)
        {
            TPM = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        TutorialSpawnPath.OnPathManage += subsidePaths;
        UITutorialGame.OnPathChanged += changedPathText;
    }
    void OnDisable()
    {
        TutorialSpawnPath.OnPathManage -= subsidePaths;
        UITutorialGame.OnPathChanged -= changedPathText;
    }

    bool subsidePaths(int value)
    {
        if(pathsUnit >= value)
        {
            pathsUnit -= value;
            return true;
        }
        return false;
    }

    int changedPathText()
    {
        return pathsUnit;
    }
}
