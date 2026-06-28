using UnityEngine;
using UnityEngine.UI;

public class AudioPanelUI : MonoBehaviour
{
    [Header("Slider")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Toggle")]
    [SerializeField] private Toggle muteToggle;

    [Header("Button")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button resetButton;

    [SerializeField] private AudioSettingsController controller;

    private float master;
    private float music;
    private float sfx;
    private bool mute;

    private void Start()
    {
        LoadCurrentSettings();

        masterSlider.onValueChanged.AddListener(OnMasterChanged);
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
        muteToggle.onValueChanged.AddListener(OnMuteChanged);

        applyButton.onClick.AddListener(OnApply);
        resetButton.onClick.AddListener(OnReset);
    }

    public void LoadCurrentSettings()
    {
        var data = SettingsManager.Instance.Data;

        master = data.MasterVolume;
        music = data.MusicVolume;
        sfx = data.SFXVolume;
        mute = data.MuteAll;

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;
        muteToggle.isOn = mute;
    }

    private void OnMasterChanged(float value)
    {
        master = value;

        controller.Preview(master, music, sfx, mute);
    }

    private void OnMusicChanged(float value)
    {
        music = value;

        controller.Preview(master, music, sfx, mute);
    }

    private void OnSFXChanged(float value)
    {
        sfx = value;

        controller.Preview(master, music, sfx, mute);
    }

    private void OnMuteChanged(bool value)
    {
        mute = value;

        controller.Preview(master, music, sfx, mute);
    }

    private void OnApply()
    {
        controller.Apply(master, music, sfx, mute);
    }

    private void OnReset()
    {
        master = 1f;
        music = 1f;
        sfx = 1f;
        mute = false;

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;
        muteToggle.isOn = mute;
    }

    public void CancelPreview()
    {
        LoadCurrentSettings();

        controller.Preview(master, music, sfx, mute);
    }
}