using UnityEngine;
using UnityEngine.UI;

public class HCSettings : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;

    [Space] [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;

    [Space] [SerializeField] private HCToggle soundToggle;
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private string privacyPolicyLink = "https://ninevastudios.com/privacy-policy.html";


    private void Awake()
    {
        InitPanel();
        InitSettings();
    }

    private void InitPanel()
    {
        openButton.onClick.AddListener(OpenPanel);
        closeButton.onClick.AddListener(ClosePanel);

        ClosePanel();
    }

    private void InitSettings()
    {
        soundToggle.onStateChanged.AddListener(SetSoundEnabled);
        //vibrationToggle.onStateChanged.AddListener(SetVibrationEnabled);
        privacyPolicyButton.onClick.AddListener(OpenPrivacyPolicy);

        var soundEnabled = true;
        DataUtility.TryLoad(ref soundEnabled, "Sound");
        soundToggle.EnabledState = soundEnabled;

        var vibrationEnabled = true;
        DataUtility.TryLoad(ref vibrationEnabled, "Vibration");
        //vibrationToggle.EnabledState = vibrationEnabled;
    }


    public void OpenPanel()
    {
        panel.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.gameObject.SetActive(false);
    }


    private void SetSoundEnabled(bool enable)
    {
        AudioListener.volume = enable ? 1.0f : 0.0f;
        DataUtility.Save(enable, "Sound");
    }

    private void SetVibrationEnabled(bool enable)
    {
        Vibration.Enabled = enable;
        DataUtility.Save(enable, "Vibration");
    }


    private void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyLink);
    }
}