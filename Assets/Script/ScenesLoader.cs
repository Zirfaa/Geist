using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Buat helper private agar tidak perlu tulis ulang di setiap method
    private void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Reset timescale sebelum pindah scene
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        LoadScene("MainMenu");
    }
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToSelectLevel()
    {
        Debug.Log("Tombol Play ditekan");
        LoadScene("SelectLevel");
    }

    public void GoToLvl1() => LoadScene("Lv1");
    public void GoToLvl2() => LoadScene("Lv2");
    public void GoToLvl3() => LoadScene("Lv3");
    public void GoToLvl4() => LoadScene("Lv4");
    public void GoToLvl5() => LoadScene("Lv5");
}