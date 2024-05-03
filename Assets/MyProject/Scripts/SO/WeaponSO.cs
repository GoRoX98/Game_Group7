using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Create Weapon")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected int _cost;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _cooldown;
    [SerializeField] protected float _range;
    [SerializeField] protected Mesh _weaponMesh;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected GameObject _projectile;
    [SerializeField] protected float _shotStrength;

    public string Name => _name;
    public int Cost => _cost;
    public int Damage => _damage;
    public float Cooldown => _cooldown;
    public float Range => _range;
    public Mesh Mesh => _weaponMesh;
    public Sprite Icon => _icon;
    public GameObject Projectile => _projectile;
    public float ShotStrength => _shotStrength;
}

