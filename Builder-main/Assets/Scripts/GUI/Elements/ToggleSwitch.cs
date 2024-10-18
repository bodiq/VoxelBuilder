using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleSwitch : Button
{
    private bool isInitialized;

    private Image toggle;
    [SerializeField] private Image handle;
    [SerializeField] private float handleRectX = 43.1f;

    [Space] [SerializeField] private Color enabledColor = Color.green;
    [SerializeField] private Color disabledColor = Color.red;
    [SerializeField] private float duration = 0.125f;

    [HideInInspector] public UnityEvent<bool> onStateChanged;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        if (isInitialized) return;
        isInitialized = true;

        toggle = GetComponent<Image>();
        onClick.AddListener(() => EnabledState = !EnabledState);
    }

    private bool isEnabled;

    public bool EnabledState
    {
        get => isEnabled;
        set
        {
            isEnabled = value;
            if (isEnabled)
                SetToggleEnabled();
            else
                SetToggleDisabled();
        }
    }

    private void SetToggleEnabled()
    {
        onStateChanged.Invoke(true);
        PlayToggleAnimation(enabledColor, handleRectX);
    }

    private void SetToggleDisabled()
    {
        onStateChanged.Invoke(false);
        PlayToggleAnimation(disabledColor, -handleRectX);
    }

    private void PlayToggleAnimation(Color toggleColor, float handleX)
    {
        Initialize();

        toggle.DOComplete();
        handle.rectTransform.DOComplete();

        toggle.DOColor(toggleColor, duration);
        handle.rectTransform.DOAnchorPosX(handleX, duration);
    }
}