using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _clearSave = false;
    [SerializeField] private float _timeAutoSave = 30f;
    [SerializeField] private List<string> _saveScenes = new List<string>();
    private GameManager _instance;

    private float _timer = 0f;
    
    public static event Action<int, int, int> LoadPlayerData;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (_clearSave)
                PlayerPrefs.DeleteAll();
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeAutoSave && _saveScenes.Contains(SceneManager.GetActiveScene().name))
        {
            _timer = 0f;
            Save();
        }
    }

    private void Save()
    {
        print("Save Data");
        PlayerPrefs.SetInt("Player_HP", SceneController.Player.Health);
        PlayerPrefs.SetInt("Player_Exp", SceneController.Player.Exp);
        PlayerPrefs.SetInt("Player_Level", SceneController.Player.Level);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        print("Load Data");
        int hp = PlayerPrefs.GetInt("Player_HP", SceneController.Player.MaxHealth);
        print($"Load HP: {hp}");
        int exp = PlayerPrefs.GetInt("Player_Exp", 0);
        int level = PlayerPrefs.GetInt("Player_Level", 1);
        LoadPlayerData?.Invoke(hp, exp, level);
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        if (!_saveScenes.Contains(arg0.name) && _saveScenes.Contains(arg1.name))
            Load();
    }
}
