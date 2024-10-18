using System;
using System.IO;
using UnityEngine;

public static class DataUtility
{
    public static string DataPath => _dataPath ??= Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    private static string _dataPath;

    public static string UserName = "User";


    private static string GetFilePath(string fileName)
    {
        return DataPath + UserName + "_" + fileName + ".json";
    }

    public static bool HasFile(string fileName)
    {
        var path = GetFilePath(fileName);
        return File.Exists(path);
    }


    public static void Save<T>(T target, string fileName)
    {
        var path = GetFilePath(fileName);
        var container = new JsonContainer<T>(target);
        var json = JsonUtility.ToJson(container);
        File.WriteAllText(path, json);
    }

    public static T Load<T>(string fileName)
    {
        var path = GetFilePath(fileName);
        var json = File.ReadAllText(path);
        var container = JsonUtility.FromJson<JsonContainer<T>>(json);
        return container.value;
    }

    public static void TryLoad<T>(ref T target, string fileName)
    {
        if (HasFile(fileName)) target = Load<T>(fileName);
    }

    [Serializable]
    private class JsonContainer<T>
    {
        public JsonContainer(T value)
        {
            this.value = value;
        }

        public T value;
    }
}