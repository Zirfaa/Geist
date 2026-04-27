using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RemainingPathData
{
    public string Level;
    public int PathsRemain;
    public int maxPaths;
}

[System.Serializable] 
public class PathData
{
    public List<RemainingPathData> pathsData = new List<RemainingPathData>();
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager saveManager;
    private string savePath;
    public PathData remainingPathData = new PathData();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(saveManager == null)
        {
            saveManager = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
        savePath = Application.persistentDataPath + "/PathRemaining.json";
        LoadData();
        Debug.Log(savePath);
    }

    public void AddData(int PathUnitsRemaining, int maxPaths)
    {
        RemainingPathData data = remainingPathData.pathsData.Find(x => x.Level == SceneManager.GetActiveScene().name);
        if(data == null)
        {
            remainingPathData.pathsData.Add(new RemainingPathData
            {
               Level = SceneManager.GetActiveScene().name,
               PathsRemain = PathUnitsRemaining,
                maxPaths = maxPaths
            });
        }else
        {
            if(PathUnitsRemaining > data.PathsRemain)
            {
                data.PathsRemain = PathUnitsRemaining;
            }
        }
        SaveData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(remainingPathData, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if(File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            remainingPathData = JsonUtility.FromJson<PathData>(json);
        }else
        {
            remainingPathData = new PathData();
        }

        if(remainingPathData == null)
        {
            remainingPathData = new PathData();
        }
        if(remainingPathData.pathsData == null)
        {
            remainingPathData.pathsData = new List<RemainingPathData>();
        }
    }

    public void RemoveData()
    {
        if(File.Exists(savePath))
        {
            File.Delete(savePath);
            remainingPathData.pathsData.Clear();
        }
    }


}
