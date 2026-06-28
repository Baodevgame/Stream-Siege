using UnityEngine;

public class AudioSettingsController : MonoBehaviour
{
    public void Preview(float master, float music, float sfx, bool mute)
    {
        AudioManager.Instance.SetMaster(master);
        AudioManager.Instance.SetMusic(music);
        AudioManager.Instance.SetSFX(sfx);
        AudioManager.Instance.SetMute(mute);
    }

    public void Apply(float master, float music, float sfx, bool mute)
    {
        Preview(master, music, sfx, mute);

        var data = SettingsManager.Instance.Data;

        data.MasterVolume = master;
        data.MusicVolume = music;
        data.SFXVolume = sfx;
        data.MuteAll = mute;

        SettingsManager.Instance.Save();
    }
}