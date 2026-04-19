using UnityEngine;

public class UIInGame : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinGamePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Priest.OnGameOver += GameOver;
        PathFinding.OnWinGame += WinGame;
    }

    void OnDisable()
    {
        Priest.OnGameOver -= GameOver;
        PathFinding.OnWinGame -= WinGame;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    void WinGame()
    {
        Time.timeScale = 0;
        WinGamePanel.SetActive(true);
    }


}
