using UnityEngine;

public class Instanceable<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null) SetupInstance();
            return _instance;
        }
    }

    public virtual void Awake()
    {
        _instance = this as T;
    }

    private static void SetupInstance()
    {
        _instance = (T)FindObjectOfType(typeof(T));
        if (_instance == null) _instance = new GameObject(typeof(T).Name).AddComponent<T>();
    }
}