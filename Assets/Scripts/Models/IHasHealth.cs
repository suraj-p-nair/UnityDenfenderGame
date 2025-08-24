public interface IHasHealth
{
    int Health { get; }
    void TakeDamage(int damage);
}
