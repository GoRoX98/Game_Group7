using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Scrollbar _loadingBar;

    public static string NextScene;

    private void Start()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(NextScene);
        op.allowSceneActivation = false;
        _loadingText.text = "Loading 0%";
        _loadingBar.size = 0;
        Loading(op);
    }

    private async void Loading(AsyncOperation op)
    {

        while (op.progress < 0.9f)
        {
            _loadingBar.size = op.progress;
            _loadingText.text = $"Loading {op.progress * 100}%";

            await Task.Delay(100);
        }

        _loadingBar.size = op.progress;
        _loadingText.text = $"Loading {op.progress * 100}%";

        await Task.Delay(1000);

        op.allowSceneActivation = true;
    }
}
