using UnityEngine;

public class PathManager : MonoBehaviour
{
    public int maxPaths;
    //public PathPlacement pathobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        PathPlacement.OnPathManage += subsidePaths;
        UIInGame.OnPathChanged += changedPathText;
    }

    void OnDisable()
    {
        PathPlacement.OnPathManage -= subsidePaths;
        UIInGame.OnPathChanged -= changedPathText;
    }

    bool subsidePaths(int value)
    {
        if(maxPaths >= value)
        {
            maxPaths -= value;
            return true;
        }
        return false;
    }

    int changedPathText()
    {
        return maxPaths;
    }
}
