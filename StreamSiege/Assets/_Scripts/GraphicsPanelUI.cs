using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsPanelUI : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Dropdown fpsDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Camera")]
    [SerializeField] private Slider fovSlider;
    [SerializeField] private Slider brightnessSlider;

    [Header("Buttons")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button resetButton;

    [SerializeField] private GraphicsSettingsController graphicsController;

    private int resolutionIndex;
    private int qualityIndex;
    private int fpsLimit;
    private bool fullscreen;
    private float fov;
    private float brightness;

    private void Start()
    {
        SetupResolutionDropdown();
        SetupQualityDropdown();
        SetupFPSDropdown();

        LoadCurrentSettings();

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        fpsDropdown.onValueChanged.AddListener(OnFPSChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);

        fovSlider.onValueChanged.AddListener(OnFOVChanged);
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        applyButton.onClick.AddListener(OnApply);
        resetButton.onClick.AddListener(OnReset);
    }
    public void CancelPreview()
    {
        var data = SettingsManager.Instance.Data;

        graphicsController.PreviewFOV(data.CameraFOV);
        graphicsController.PreviewBrightness(data.Brightness);

        LoadCurrentSettings();
    }
    #region Setup

    private void SetupResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();

        List<string> options = new();

        foreach (Vector2Int res in graphicsController.GetResolutions())
        {
            options.Add($"{res.x} x {res.y}");
        }

        resolutionDropdown.AddOptions(options);
    }

    private void SetupQualityDropdown()
    {
        qualityDropdown.ClearOptions();

        qualityDropdown.AddOptions(new List<string>()
        {
            "Low",
            "Medium",
            "High"
        });
    }

    private void SetupFPSDropdown()
    {
        fpsDropdown.ClearOptions();

        fpsDropdown.AddOptions(new List<string>()
        {
            "60 FPS",
            "120 FPS",
            "144 FPS",
            "Unlimited"
        });
    }

    #endregion

    public void LoadCurrentSettings()
    {
        var data = SettingsManager.Instance.Data;

        resolutionIndex = data.ResolutionIndex;
        qualityIndex = data.QualityIndex;
        fpsLimit = data.FPSLimit;
        fullscreen = data.Fullscreen;
        fov = data.CameraFOV;
        brightness = data.Brightness;

        resolutionDropdown.value = resolutionIndex;
        qualityDropdown.value = qualityIndex;
        fullscreenToggle.isOn = fullscreen;

        fovSlider.value = fov;
        brightnessSlider.value = brightness;

        switch (fpsLimit)
        {
            case 60:
                fpsDropdown.value = 0;
                break;

            case 120:
                fpsDropdown.value = 1;
                break;

            case 144:
                fpsDropdown.value = 2;
                break;

            default:
                fpsDropdown.value = 3;
                break;
        }
    }

    #region Events

    private void OnResolutionChanged(int index)
    {
        resolutionIndex = index;
    }

    private void OnQualityChanged(int index)
    {
        qualityIndex = index;
    }

    private void OnFPSChanged(int index)
    {
        switch (index)
        {
            case 0:
                fpsLimit = 60;
                break;

            case 1:
                fpsLimit = 120;
                break;

            case 2:
                fpsLimit = 144;
                break;

            default:
                fpsLimit = -1;
                break;
        }
    }

    private void OnFullscreenChanged(bool value)
    {
        fullscreen = value;
    }

    private void OnFOVChanged(float value)
    {
        fov = value;

        graphicsController.PreviewFOV(value);
    }

    private void OnBrightnessChanged(float value)
    {
        brightness = value;

        graphicsController.PreviewBrightness(value);
    }

    private void OnApply()
    {
        graphicsController.SetFullscreen(fullscreen);
        graphicsController.SetResolution(resolutionIndex, fullscreen);
        graphicsController.SetQuality(qualityIndex);
        graphicsController.SetFPS(fpsLimit);

        var data = SettingsManager.Instance.Data;

        data.Fullscreen = fullscreen;
        data.ResolutionIndex = resolutionIndex;
        data.QualityIndex = qualityIndex;
        data.FPSLimit = fpsLimit;
        data.CameraFOV = fov;
        data.Brightness = brightness;

        graphicsController.PreviewFOV(fov);
        graphicsController.PreviewBrightness(brightness);

        SettingsManager.Instance.Save();
    }

    private void OnReset()
    {
        resolutionIndex = 2;
        qualityIndex = 2;
        fpsLimit = 120;
        fullscreen = true;
        fov = 60;
        brightness = 1;

        resolutionDropdown.value = resolutionIndex;
        qualityDropdown.value = qualityIndex;
        fullscreenToggle.isOn = fullscreen;
        fovSlider.value = fov;
        brightnessSlider.value = brightness;
        fpsDropdown.value = 1;
    }

    #endregion
}