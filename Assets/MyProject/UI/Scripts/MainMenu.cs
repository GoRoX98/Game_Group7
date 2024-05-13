using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static event Action NewGame;

    public void TestScene()
    {
        SceneManager.LoadSceneAsync("TestScene");
    }

    public void StartNewGame()
    {
        NewGame?.Invoke();
        LoadingScene.NextScene = "SafeZone";
        SceneManager.LoadScene("Loading");
    }
}
