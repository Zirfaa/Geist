using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingManagers : MonoBehaviour
{
    public GameObject settingPanel;
    public Slider MasterVolSlider;
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;
    void Start()
    {
        settingPanel.SetActive(false);
        SetSliderVolume();
    }
    public void OpenSetting()
    {
        
        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

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
}