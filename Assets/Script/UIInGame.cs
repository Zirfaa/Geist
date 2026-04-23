using System;
using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinGamePanel;
    public TextMeshProUGUI PathUnits;
    public static event Func<int> OnPathChanged;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int pathsValue = OnPathChanged?.Invoke() ?? 0;
        PathUnits.text = "Path Units : " + pathsValue;
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
