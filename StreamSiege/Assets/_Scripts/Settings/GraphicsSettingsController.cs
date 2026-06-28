using UnityEngine;

public class GraphicsSettingsController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light directionalLight;

    private readonly Vector2Int[] resolutions =
    {
        new Vector2Int(1280,720),
        new Vector2Int(1600,900),
        new Vector2Int(1920,1080)
    };
    
    public Vector2Int[] GetResolutions()
    {
        return resolutions;
    }

    public void SetResolution(int index, bool fullscreen)
    {
        if (index < 0 || index >= resolutions.Length)
            return;

        Vector2Int res = resolutions[index];

        Screen.SetResolution(res.x, res.y, fullscreen);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFPS(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void SetFullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void SetFOV(float value)
    {
        value = Mathf.Clamp(value, 40, 70);

        if (mainCamera != null)
            mainCamera.fieldOfView = value;
    }

    public void SetBrightness(float value)
    {
        value = Mathf.Clamp(value, 0.2f, 2f);

        if (directionalLight != null)
            directionalLight.intensity = value;
    }
    public void PreviewFOV(float value)
    {
        value = Mathf.Clamp(value, 40f, 70f);

        if (mainCamera != null)
            mainCamera.fieldOfView = value;
    }

    public void PreviewBrightness(float value)
    {
        value = Mathf.Clamp(value, 0.2f, 2f);

        if (directionalLight != null)
            directionalLight.intensity = value;
    }
}