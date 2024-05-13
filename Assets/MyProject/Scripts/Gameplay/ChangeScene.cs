using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private bool _canChangeScene = false;

    // Update is called once per frame
    void Update()
    {
        if (_canChangeScene && Input.GetKeyDown(KeyCode.E))
        {
            LoadingScene.NextScene = _sceneName;
            SceneManager.LoadScene("Loading");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
            _canChangeScene = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
            _canChangeScene = false;
    }
}
