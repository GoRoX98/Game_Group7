using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private int _maxHealth;
    private int _health;

    public int Health => _health;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    private void Start()
    {
        _health = _maxHealth;
    }

    private void Die()
    {
        print("Dead");
    }

    private void OnHeal(object sender, int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    private void OnTakeDmg(object sender, int damage)
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
