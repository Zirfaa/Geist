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

    public void GoToGame()
    {
        SceneManager.LoadScene("InGame");
    }
}