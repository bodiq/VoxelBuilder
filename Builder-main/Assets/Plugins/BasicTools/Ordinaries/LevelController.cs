using DG.Tweening;
using UnityEngine.SceneManagement;

public static class LevelController
{
    public const int HomeIndex = 0;

    public static void Restart()
    {
        var scene = SceneManager.GetActiveScene();
        Load(scene.buildIndex);
    }

    public static void Next()
    {
        var scene = SceneManager.GetActiveScene();
        Load(scene.buildIndex + 1);
    }

    public static void Load(int levelIndex)
    {
        DOTween.KillAll();

        if (levelIndex >= SceneManager.sceneCountInBuildSettings) levelIndex = HomeIndex;
        SceneManager.LoadScene(levelIndex);
    }

    public static void Home()
    {
        Load(HomeIndex);
    }
}