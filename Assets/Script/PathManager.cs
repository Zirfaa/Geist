using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager pathManager;
    public int maxPaths;
    [HideInInspector] public int pathsUnit;
    public bool canSpawnPath = true;
    //public PathPlacement pathobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(pathManager == null)
        {
            pathManager = this;
        }
    }
    void Start()
    {
        pathsUnit = maxPaths;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        SpawnPath.OnPathManage += subsidePaths;
        UIInGame.OnPathChanged += changedPathText;
        UIInGame.OnSavePathRemaining += SaveData;
    }

    void OnDisable()
    {
        SpawnPath.OnPathManage -= subsidePaths;
        UIInGame.OnPathChanged -= changedPathText;
        UIInGame.OnSavePathRemaining -= SaveData;
    }

    void SaveData()
    {
        SaveManager.saveManager.AddData(pathsUnit, maxPaths);
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
