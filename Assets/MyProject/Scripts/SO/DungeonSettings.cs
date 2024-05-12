using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SceneSettings", menuName = "ScriptableObjects/Create Dungeon Settings")]
public class DungeonSettings : SceneSettings
{
    [SerializeField] private List<GameObject> _enemiesPrefabs;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private List<LootSO> _lootVariants = new List<LootSO>();
    [SerializeField] private int _lootCount;

    public List<GameObject> EnemiesPrefabs => _enemiesPrefabs;
    public int EnemiesCount => _enemiesCount;
    public int LootCount => _lootCount;
    public GameObject LootPrefab => _lootPrefab;
    public List<LootSO> LootVariants => _lootVariants;
}
