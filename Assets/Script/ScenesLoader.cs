using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void LoadScene(string sceneName)
    {
        GameManager.instance.timer = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        GameManager.instance.timer = 0f;
        LoadScene("MainMenu");
    }
    public void ReloadCurrentScene()
    {
        GameManager.instance.timer = 0f;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReloadPath()
    {
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        if(paths == null) return;
        foreach(GameObject path in paths)
        {
            PathManager.pathManager.pathsUnit = PathManager.pathManager.maxPaths;
            Destroy(path);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToSelectLevel()
    {
        Debug.Log("Tombol Play ditekan");
        LoadScene("SelectLevel");
    }

    public void GoToNewGame()
    {
        SaveManager.saveManager.RemoveData();
        LoadScene("SelectLevel");
    }

    public void GoToLvl1() => LoadScene("Lv1");
    public void GoToLvl2() => LoadScene("Lv2");
    public void GoToLvl3() => LoadScene("Lv3");
    public void GoToLvl4() => LoadScene("Lv4");
    public void GoToLvl5() => LoadScene("Lv5");
}