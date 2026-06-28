using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _panelSetting;
    public GameObject _panelGraphic;
    public GameObject _panelAudio;
    public GameObject _panelHotkey;

    [SerializeField] private GraphicsPanelUI graphicsPanelUI;
    [SerializeField] private AudioPanelUI audioPanelUI;

    private void Awake()
    {
        _panelSetting.SetActive(false);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            _panelSetting.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Close()
    {
        graphicsPanelUI.CancelPreview();
        audioPanelUI.CancelPreview();

        _panelSetting.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
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
