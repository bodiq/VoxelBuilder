using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Params : SerializedDictionary<string, object>
{
    public static readonly Params Empty = new();
}

public class EventManager : Singleton<EventManager>
{
    [SerializeField] private SerializedDictionary<string, UnityEvent<Params>> eventDictionary = new();
    [SerializeField] private List<string> eventDisabledList = new();


    public static void AddListener(string eventName, UnityAction<Params> call)
    {
        if (Instance.eventDictionary.ContainsKey(eventName))
        {
            Instance.eventDictionary[eventName].AddListener(call);
        }
        else
        {
            Instance.eventDictionary.Add(eventName, new UnityEvent<Params>());
            Instance.eventDictionary[eventName].AddListener(call);
        }
    }

    public static void RemoveListener(string eventName, UnityAction<Params> call)
    {
        if (Instance.eventDictionary.ContainsKey(eventName)) Instance.eventDictionary[eventName].RemoveListener(call);
    }

    public static void Invoke(string eventName, Params eventParams = null)
    {
        if (Instance.eventDisabledList.Contains(eventName)) return;
        if (Instance.eventDictionary.ContainsKey(eventName)) Instance.eventDictionary[eventName].Invoke(eventParams ?? Params.Empty);
    }


    public static bool IsEnabled(params string[] eventNames)
    {
        return eventNames.All(eventName => !Instance.eventDisabledList.Contains(eventName));
    }

    public static void Disable(params string[] eventNames)
    {
        foreach (var eventName in eventNames)
        {
            if (Instance.eventDisabledList.Contains(eventName)) continue;
            Instance.eventDisabledList.Add(eventName);
        }
    }

    public static void Enable(params string[] eventNames)
    {
        foreach (var eventName in eventNames) Instance.eventDisabledList.Remove(eventName);
    }

    public static void DisableAll()
    {
        Instance.eventDisabledList.AddRange(Instance.eventDictionary.Keys);
    }

    public static void EnableAll()
    {
        Instance.eventDisabledList.Clear();
    }
}