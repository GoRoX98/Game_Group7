using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    [Header("Enemy Options")]
    [SerializeField] protected int _maxHealth = 100;
    protected int _currentHealth;

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
        if (_currentHealth < _maxHealth)
            _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    protected virtual void OnTakeDmg(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (_currentHealth > damage)
            _currentHealth -= damage;
        else
        {
            _currentHealth = 0;
            Die();
        }

        print($"Health: {_currentHealth} | Dmg: {damage}");
    }

}
