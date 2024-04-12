using UnityEngine;

public class Enemy : MonoBehaviour, IDamageble
{
    [SerializeField] private int _maxHealth;
    private int _health;

    public int Health => _health;

    private void Start()
    {
        _health = _maxHealth;
    }

    #region IDamagble

    public void Die()
    {
        print("Dead");
    }

    public void Heal(int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void TakeDmg(int damage)
    {
        if (_health > damage)
            _health -= damage;
        else
        {
            _health = 0;
            Die();
        }

        print($"Health: {_health} | Dmg: {damage}");
    }

    #endregion

}
