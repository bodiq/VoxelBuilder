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
        if (_instance == null) Debug.LogError("Has no " + typeof(T) + " instance on scene, but you try to get it!");
    }
}