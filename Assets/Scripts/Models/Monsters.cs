using UnityEngine;
using System;

public class Monsters : MonoBehaviour
{
    public MonsterType monsterType;
    public int health;
    public float speed;
    public event Action OnMonsterDestroyed;

    public bool IsDead => health <= 0;

    // Initialize called by MonsterFactory
    public virtual void Initialize(MonsterType type, int health, float speed)
    {
        this.monsterType = type;
        this.health = health;
        this.speed = speed;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (IsDead)
            OnMonsterDestroyed?.Invoke();
    }
}
