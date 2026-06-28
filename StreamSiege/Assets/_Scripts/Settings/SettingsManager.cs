using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public SettingsData Data;

    private const string SETTINGS_KEY = "SETTINGS";

    private void Awake()
    {
        Instance = this;

        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data);

        PlayerPrefs.SetString(SETTINGS_KEY, json);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SETTINGS_KEY))
        {
            string json = PlayerPrefs.GetString(SETTINGS_KEY);

            Data = JsonUtility.FromJson<SettingsData>(json);
        }
        else
        {
            Data = new SettingsData();
        }
    }
}
