public interface IDamageble
{
    public int Health { get; }

    public void TakeDmg(int damage);
    public void Heal(int heal);
    public void Die();
}
