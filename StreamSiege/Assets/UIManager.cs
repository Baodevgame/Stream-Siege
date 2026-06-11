using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _panelSetting;
    public GameObject _panelGraphic;
    public GameObject _panelAudio;
    public GameObject _panelHotkey;

    private void Awake()
    {
        _panelSetting.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            _panelSetting.SetActive(true);
        }
    }

    public void Close()
    {
        _panelSetting.SetActive(false);
    }

    public void OnGraphicPanel()
    {
        SwitchPanel(_panelGraphic);
        Debug.Log("click button Graphic");
    }
    public void OnAudioPanel()
    {
        SwitchPanel(_panelAudio);
        Debug.Log("click button Audio");
    }
    public void OnHotkeyPanel()
    {
        SwitchPanel(_panelHotkey);
        Debug.Log("click button Hotkey");
    }

    public void SwitchPanel(GameObject panelToActive)
    {
        _panelGraphic.SetActive(false);
        _panelAudio.SetActive(false);
        _panelHotkey.SetActive(false);

        if(panelToActive != null)
        {
            panelToActive.SetActive(true);
        }
    }
}
