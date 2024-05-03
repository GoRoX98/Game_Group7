using UnityEngine;

public class Attacker : MonoBehaviour
{
    private bool CanAttack => _attackTime <= 0;

    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _damageMask;
    [SerializeField] private WeaponSO _weapon;
    [SerializeField] private Transform _projectile;
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

    //��� ��� ����� ������������� ������, ������ ��������� �� �������
    public void Attack()
    {
        if (_weapon.Range <= 3 && CanAttack)
            MeleeAttack();
        else if (CanAttack)
            RangeAttack();
    }

    private void MeleeAttack()
    {
        var index = Random.Range(0, 2);
        _animator.SetInteger("AttackVariant", index);
        _animator.SetTrigger("Attack");
        ResetAttackTimer();
        AttackNear();
    }

    private void RangeAttack()
    {
        var index = Random.Range(0, 2);
        _animator.SetInteger("AttackVariant", index);
        _animator.SetTrigger("Attack");
        ResetAttackTimer();

        Rigidbody rb = Instantiate(_weapon.Projectile, _projectile.position, transform.rotation).GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.forward * _weapon.ShotStrength, ForceMode.Impulse);
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
