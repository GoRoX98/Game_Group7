using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void TestScene()
    {
        SceneManager.LoadSceneAsync("TestScene");
    }
}
