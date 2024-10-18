using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        DataUtility.TryLoad(ref LevelCount, "LevelCounter");
        var targetLevelName = "Level " + LevelCount;
        if (SceneManager.GetActiveScene().name != targetLevelName) SceneManager.LoadScene(targetLevelName);
#endif
    }

    public void ChangeLevel()
    {
        if (LevelCount == 13)
        {
            LevelCount = 1;
            ModelCreating.ResetStaticValues();
            DataUtility.Save(LevelCount, "LevelCounter");
            SceneManager.LoadScene("Level 1");
            return;
        }
        LevelCount++;
        ModelCreating.ResetStaticValues();
        DataUtility.Save(LevelCount, "LevelCounter");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static int GetLevelCount()
    {
        return LevelCount;
    }

    private static int LevelCount = 1;
}
