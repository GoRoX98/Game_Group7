using UnityEngine;

[CreateAssetMenu(fileName = "New Loot", menuName = "ScriptableObjects/Create Loot Item")]
public class LootSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Mesh _mesh;

    public string Name => _name;
    public int Cost => _cost;
    public Sprite Icon => _icon;
    public Mesh Mesh => _mesh;
}
