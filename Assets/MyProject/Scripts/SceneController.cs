using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private SceneSettings _settings;
    [SerializeField] private List<Transform> _spawnLootPoints = new List<Transform>();
    [SerializeField] private List<Transform> _spawnEnemyPoints = new List<Transform>();
    

    private Player _player;
    private Transform _playerTransform;

    public static Transform PlayerTransform;
    public static Vector3 PlayerPos => PlayerTransform.position;
    public static Player Player;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        else
            SpawnPlayer();

        _playerTransform = _player.transform;
        PlayerTransform = _playerTransform;
        Player = _player;

        _player.gameObject.SetActive(false);
        _playerTransform.SetPositionAndRotation(_settings.StartPosition, Quaternion.identity);
        _player.gameObject.SetActive(true);
    }

    private void Start()
    {
        if (_settings is DungeonSettings settings)
            GenerateDungeon(settings);

    }

    private void SpawnPlayer()
    {
        _player = Instantiate(_playerPrefab, _settings.StartPosition, Quaternion.identity).GetComponent<Player>();
    }

    private void GenerateDungeon(DungeonSettings settings)
    {
        List<Transform> lootPoints = new List<Transform>(_spawnLootPoints);
        List<Transform> enemyPoints = new List<Transform>(_spawnEnemyPoints);

        for (int i = 0; i < settings.LootCount; i++)
        {
            Vector3 position;
            if (lootPoints.Count == 0)
                break;
            else
            {
                Transform point = lootPoints[Random.Range(0, lootPoints.Count)];
                position = point.position;
                lootPoints.Remove(point);
            }

            int index = Random.Range(0, settings.LootVariants.Count);

            Loot loot = Instantiate(settings.LootPrefab, position, Quaternion.identity).GetComponent<Loot>();
            loot.Init(settings.LootVariants[index]);
        }

        for (int i = 0; i < settings.EnemiesCount; i++)
        {
            Vector3 position;
            if (enemyPoints.Count < 4)
                enemyPoints = new(_spawnEnemyPoints);
            
            Transform point = enemyPoints[Random.Range(0, enemyPoints.Count)];
            position = point.position;
            enemyPoints.Remove(point);
            

            int index = Random.Range(0, settings.EnemiesPrefabs.Count);
            Instantiate(settings.EnemiesPrefabs[index], position, Quaternion.identity);
        }
    }
}
