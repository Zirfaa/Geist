using System;
using TMPro;
using UnityEngine;

public class UIInGame : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinGamePanel;
    public GameObject DestroyPathPanel;
    public TextMeshProUGUI PathUnits;
    public static event Func<int> OnPathChanged;
    public static event Action OnSavePathRemaining;
    public AudioClip GameWin;
    public AudioClip GameLose;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
        DestroyPathPanel.SetActive(false);
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
        SpawnPath.OnDestroyPanelShow += RemovePathPanel;
        PathPlacement.OnDestroyPanelHide += RemovePathPanel;
    }

    void OnDisable()
    {
        Priest.OnGameOver -= GameOver;
        PathFinding.OnWinGame -= WinGame;
        SpawnPath.OnDestroyPanelShow -= RemovePathPanel;
        PathPlacement.OnDestroyPanelHide -= RemovePathPanel;
    }

    void GameOver()
    {
        AudioManager.audioManager.PlaySFX(GameLose);
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    void WinGame()
    {
        AudioManager.audioManager.PlaySFX(GameWin);
        Time.timeScale = 0;
        WinGamePanel.SetActive(true);
        OnSavePathRemaining?.Invoke();
    }

    void RemovePathPanel(bool isActive)
    {
        DestroyPathPanel.SetActive(isActive);
    }

    // void DeestroyPanelHide()
    // {
    //     DestroyPathPanel.SetActive(false);
    // }


}
