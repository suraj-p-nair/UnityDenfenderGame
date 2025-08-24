using UnityEngine;
using System;

public class Monsters : MonoBehaviour, IHasHealth
{
    public int _health;
    public float _speed;
    public int _damage;

    public int Health => _health;
    public event Action OnMonsterDestroyed;

    public void Initialize(MonsterStats stats)
    {
        _health = stats.health;
        _speed = stats.speed;
        _damage = stats.damage;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Projectiles>(out var projectile))
        {
            TakeDamage(projectile.damage);
            Destroy(collision.gameObject);
    }
    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        if (_health <= 0)
            OnMonsterDestroyed?.Invoke();
    }
}
