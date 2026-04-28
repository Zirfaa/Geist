using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public AudioMixer audioMixer;
    public AudioSource MusicVolume;
    public AudioSource SFXVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(audioManager == null)
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadAudio();
        PlayBGM();
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("VolMaster", ToMixerVolume(value));
        PlayerPrefs.SetFloat("VolMaster", value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("VolMusic", ToMixerVolume(value));
        PlayerPrefs.SetFloat("VolMusic", value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("VolSFX", ToMixerVolume(value));
        PlayerPrefs.SetFloat("VolSFX", value);
    }

    public void PlayBGM()
    {
        MusicVolume.Play();
    }

    public void StopBGM()
    {
        MusicVolume.Stop();
    }

    public void PlaySFX(AudioClip audio)
    {
        SFXVolume.PlayOneShot(audio);
    }

    float ToMixerVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        return Mathf.Log10(value) * 20;
    }

    public void LoadAudio()
    {
        float master = PlayerPrefs.GetFloat("VolMaster", 1f);
        float music = PlayerPrefs.GetFloat("VolMusic", 1f);
        float sfx = PlayerPrefs.GetFloat("VolSFX", 1f);

        audioMixer.SetFloat("VolMaster", ToMixerVolume(master));
        audioMixer.SetFloat("VolMusic", ToMixerVolume(music));
        audioMixer.SetFloat("VolSFX", ToMixerVolume(sfx));
    }
}
