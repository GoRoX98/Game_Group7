using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;
    [SerializeField] private WeaponSO _weapon;
    [SerializeField] private MeshFilter _weaponMeshFilter;

    private Collider[] _hits = new Collider[3];
    private float _attackTime;

    public float AttackRadius => _weapon.Range;
    public int Damage => _weapon.Damage;
    public float Cooldown => _weapon.Cooldown;

    private void Start()
    {
        ResetAttackTimer();

        _weaponMeshFilter.mesh = _weapon.Mesh;
    }

    void Update()
    {
        if (!CanAttack)
        {
            _attackTime -= Time.deltaTime;
        }
    }

    //Так как будет дистанционная атакая, делаем прослойку на будущее
    public void Attack()
    {
        MeleeAttack();
    }

    private void MeleeAttack()
    {
        if (!CanAttack)
            return;

        var index = Random.Range(0, 2);
        _animator.SetInteger("AttackVariant", index);
        _animator.SetTrigger("Attack");
        ResetAttackTimer();
        AttackNear();
    }

    private void AttackNear()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, AttackRadius, _hits, _damageMask);

        for (int i = 0; i < count; i++)
        {
            if (_hits[i].TryGetComponent<IDamageble>(out var damagble))
            {
                damagble.TakeDamage?.Invoke(this, Damage);
            }
        }
    }

    private void ResetAttackTimer() => _attackTime = Cooldown;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}
