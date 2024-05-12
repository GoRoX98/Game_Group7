using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private SceneSettings _settings;
    [SerializeField] private List<Transform> _spawnLootPoints = new List<Transform>();
    [SerializeField] private List<Transform> _spawnEnemyPoints = new List<Transform>();
    

    private static Player _player;
    private static Transform _playerTransform;

    public static Vector3 PlayerPos => _playerTransform.position;
    public static Transform PlayerTransform => _playerTransform;
    public static Player Player => _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerTransform = _player.transform;
    }

    private void Start()
    {
        if (_settings is DungeonSettings settings)
            GenerateDungeon(settings);

        _playerTransform.position = _settings.StartPosition;
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
