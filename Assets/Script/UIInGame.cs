using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject WinGamePanel;
    public GameObject DestroyPathPanel;
    public TextMeshProUGUI PathUnits;
    public TextMeshProUGUI TimerLeft;
    public static event Func<int> OnPathChanged;
    public static event Action OnSavePathRemaining;
    public static event Func<int> OnTimerChange;
    public AudioClip GameWin;
    public AudioClip GameLose;
    public Slider MasterVolSlider;
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;
    public GameObject PausePanel;
    public GameObject Setting;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverPanel.SetActive(false);
        WinGamePanel.SetActive(false);
        DestroyPathPanel.SetActive(false);
        PausePanel.SetActive(false);
        Setting.SetActive(false);
        SetSliderVolume();
    }

    // Update is called once per frame
    void Update()
    {
        int pathsValue = OnPathChanged?.Invoke() ?? 0;
        int timerChange = OnTimerChange?.Invoke() ?? 0;
        if(timerChange < 1)
        {
            timerChange = 0;
        }else if(timerChange <= 5)
        {
            TimerLeft.color = Color.red;
        }else if(timerChange > 5)
        {
            TimerLeft.color = Color.green;
        }
        PathUnits.text = "Path Units : " + pathsValue;
        TimerLeft.text = timerChange.ToString();
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
        GameManager.instance.timer = 0;
        AudioManager.audioManager.PlaySFX(GameLose);
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }

    void WinGame()
    {
        GameManager.instance.timer = 0;
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

    void SetSliderVolume()
    {
        float master = PlayerPrefs.GetFloat("VolMaster", 1f);
        float music = PlayerPrefs.GetFloat("VolMusic", 1f);
        float sfx = PlayerPrefs.GetFloat("VolSFX", 1f);
        
        MasterVolSlider.value = master;
        MusicVolSlider.value = music;
        SFXVolSlider.value = sfx;

        AudioManager.audioManager.SetMasterVolume(master);
        AudioManager.audioManager.SetMusicVolume(music);
        AudioManager.audioManager.SetSFXVolume(sfx);

        MasterVolSlider.onValueChanged.AddListener(AudioManager.audioManager.SetMasterVolume);
        MusicVolSlider.onValueChanged.AddListener(AudioManager.audioManager.SetMusicVolume);
        SFXVolSlider.onValueChanged.AddListener(AudioManager.audioManager.SetSFXVolume);
    }

    public void OpenPausePanel()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void OpenSetting()
    {
        Setting.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void CloseSetting()
    {
        Setting.SetActive(false);
        PausePanel.SetActive(true);
    }

}
