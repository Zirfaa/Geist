using UnityEngine;

public class UIInGame : MonoBehaviour
{
    public GameObject GameOverPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        Priest.OnGameOver += GameOver;
    }

    void OnDisable()
    {
        Priest.OnGameOver -= GameOver;
    }

    void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }


}
