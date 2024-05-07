using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    [Header("Enemy Options")]
    [SerializeField] protected float _visionRadius = 10f;
    [SerializeField] protected int _level = 1;
    [SerializeField] protected CharProgressSO _progressSO;
    [SerializeField] protected ParticleSystem _damageParticles;

    protected CharCharacteristics _charData => _progressSO.CurrentLevelData(_level);
    protected int _currentHealth;

    public int MaxHealth => _charData.MaxHP;
    public float MaxSpeed => _charData.MaxSpeed;
    public float MinSpeed => _charData.MinSpeed;
    public float SpeedIncrase => _charData.SpeedIncrase;
    public int Health => _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    protected virtual void Die()
    {
        print("Dead");
    }

    protected virtual void OnHeal(object sender, int heal)
    {
        if (_currentHealth < MaxHealth)
            _currentHealth += heal;

        if (_currentHealth > MaxHealth)
            _currentHealth = MaxHealth;
    }

    protected virtual void OnTakeDmg(object sender, int damage)
    {
        if (sender is not Attacker && sender is not Spell)
            return;

        if (_currentHealth > damage)
            _currentHealth -= damage;
        else
        {
            _currentHealth = 0;
            Die();
        }

        print($"Health: {_currentHealth} | Dmg: {damage}");
        _damageParticles.Play();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
    }

}
