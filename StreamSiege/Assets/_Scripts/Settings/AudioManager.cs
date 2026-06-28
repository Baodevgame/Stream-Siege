using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMaster(float value)
    {
        if (value <= 0f)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
            return;
        }

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
    }

    public void SetMusic(float value)
    {
        if (value <= 0f)
        {
            audioMixer.SetFloat("MusicVolume", -80f);
            return;
        }

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
    }

    public void SetSFX(float value)
    {
        if (value <= 0f)
        {
            audioMixer.SetFloat("SFXVolume", -80f);
            return;
        }

        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
    }

    public void SetMute(bool mute)
    {
        if (mute)
            audioMixer.SetFloat("MasterVolume", -80);
        else
            SetMaster(SettingsManager.Instance.Data.MasterVolume);
    }
}