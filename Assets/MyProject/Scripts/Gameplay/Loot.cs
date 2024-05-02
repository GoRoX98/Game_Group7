using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    private LootSO _lootData;

    public LootSO LootData => _lootData;

    public void Init(LootSO data)
    {
        if (_lootData != null)
            return;

        _lootData = data;
        _meshFilter.mesh = _lootData.Mesh;
    }

    public void OnPickUp()
    {
        Destroy(gameObject);
    }

}
