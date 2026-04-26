using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToSelectLevel()
    {
        Debug.Log("Tombol Play ditekan");
        SceneManager.LoadScene("SelectLevel");
    }

    public void GoToLvl1()
    {
        SceneManager.LoadScene("Lv1");
    }
    public void GoToLvl2()
    {
        SceneManager.LoadScene("Lv2");
    }
    public void GoToLvl3()
    {
        SceneManager.LoadScene("Lv3");
    }
    public void GoToLvl4()
    {
        SceneManager.LoadScene("Lv4");
    }
    public void GoToLvl5()
    {
        SceneManager.LoadScene("Lv5");
    }
}