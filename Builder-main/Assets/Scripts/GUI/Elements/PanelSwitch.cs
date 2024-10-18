using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PanelSwitch : MonoBehaviour
{
    [SerializeField] private bool closeOnAwake = true;

    [HideInInspector] public UnityEvent onOpened = new();
    [HideInInspector] public UnityEvent onClosed = new();

    protected virtual void Awake()
    {
        if (closeOnAwake) transform.localScale = new Vector3(1.0f, 0.0f, 1.0f);
    }

    public virtual void Open()
    {
        transform.DOKill();
        transform.DOScaleY(1.0f, 0.125f).OnComplete(onOpened.Invoke);
    }

    public virtual void Close()
    {
        transform.DOKill();
        transform.DOScaleY(0.0f, 0.125f).OnComplete(onClosed.Invoke);
    }
}