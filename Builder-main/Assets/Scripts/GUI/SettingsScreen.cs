using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;

    [Space] [SerializeField] private Button openButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private float fadeDuration = 0.125f;

    [Space] [SerializeField] private ToggleSwitch soundToggle;
    [SerializeField] private ToggleSwitch vibrationToggle;
    [SerializeField] private Button privacyPolicyButton;
    [SerializeField] private string privacyPolicyLink = "https://ninevastudios.com/privacy-policy.html";

    [Space] [SerializeField] private Button resetButton;

    private const string SoundName = "Sound";
    private const string VibrationName = "Vibration";


    private void Awake()
    {
        InitSettingPanel();
        InitSettingsOptions();
        InitResetButton();
    }


    private void InitSettingPanel()
    {
        panel.alpha = 0.0f;
        panel.gameObject.SetActive(false);

        openButton.onClick.AddListener(OpenSettingsPanel);
        closeButton.onClick.AddListener(CloseSettingsPanel);
    }

    public void OpenSettingsPanel()
    {
        panel.DOComplete();
        panel.DOFade(1.0f, fadeDuration).OnStart(() => panel.gameObject.SetActive(true));
    }

    public void CloseSettingsPanel()
    {
        panel.DOComplete();
        panel.DOFade(0.0f, fadeDuration).OnComplete(() => panel.gameObject.SetActive(false));
    }


    private void InitSettingsOptions()
    {
        soundToggle.onStateChanged.AddListener(SetSoundEnabled);
        vibrationToggle.onStateChanged.AddListener(SetVibrationEnabled);
        privacyPolicyButton.onClick.AddListener(OpenPrivacyPolicy);

        var soundEnabled = true;
        DataUtility.TryLoad(ref soundEnabled, SoundName);
        soundToggle.EnabledState = soundEnabled;

        var vibrationEnabled = true;
        DataUtility.TryLoad(ref vibrationEnabled, VibrationName);
        vibrationToggle.EnabledState = vibrationEnabled;
    }

    private void SetSoundEnabled(bool enable)
    {
        AudioListener.volume = enable ? 1.0f : 0.0f;
        DataUtility.Save(enable, SoundName);
    }

    private void SetVibrationEnabled(bool enable)
    {
        Vibration.Enabled = enable;
        DataUtility.Save(enable, VibrationName);
    }

    private void OpenPrivacyPolicy()
    {
        Application.OpenURL(privacyPolicyLink);
    }


    private void InitResetButton()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        resetButton.gameObject.SetActive(true);
#else
        resetButton.gameObject.SetActive(false);
#endif
    }
    
}