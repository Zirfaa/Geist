using UnityEngine;

public class PathManager : MonoBehaviour
{
    public int maxPaths;
    [HideInInspector] public int pathsUnit;
    //public PathPlacement pathobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        PathPlacement.OnPathManage += subsidePaths;
        UIInGame.OnPathChanged += changedPathText;
        UIInGame.OnSavePathRemaining += SaveData;
    }

    void OnDisable()
    {
        PathPlacement.OnPathManage -= subsidePaths;
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
