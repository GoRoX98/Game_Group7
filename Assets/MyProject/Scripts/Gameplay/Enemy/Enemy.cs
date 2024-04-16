using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    [Header("Enemy Options")]
    [SerializeField] protected int _maxHealth = 100;
    protected int _health;

    public int Health => _health;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    protected virtual void Die()
    {
        print("Dead");
    }

    protected virtual void OnHeal(object sender, int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    protected virtual void OnTakeDmg(object sender, int damage)
    {
        if (sender is not Attacker)
            return;

        if (_health > damage)
            _health -= damage;
        else
        {
            _health = 0;
            Die();
        }

        print($"Health: {_health} | Dmg: {damage}");
    }

}
