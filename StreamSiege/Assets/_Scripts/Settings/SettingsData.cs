[System.Serializable]
public class SettingsData
{
    public float MasterVolume = 1f;
    public float MusicVolume = 1f;
    public float SFXVolume = 1f;

    public int ResolutionIndex = 2;

    public int QualityIndex = 2;

    public int FPSLimit = 120;

    public bool Fullscreen = true;
    public bool MuteAll = false;

    public float CameraFOV = 60f;

    public float Brightness = 1f;
}