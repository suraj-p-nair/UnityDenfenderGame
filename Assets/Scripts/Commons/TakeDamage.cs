using Assets.Interface;
using Assets.Models;
using Assets.Scripts;
using Assets.Scripts.Monster;
using Assets.Scripts.Projectile;
using TMPro;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private IHealth _statsProvider;  // Health of this object
    private int _damage;              // Damage this object deals (for monsters or melee player)
    private void Awake()
    {
        // If this is a monster, get its stats
        if (TryGetComponent<BasicMonster>(out var monster))
        {
            _statsProvider = monster.MonsterStats;
            _damage = monster.MonsterStats.Damage;
            return;
        }

        // If this is a player, get its stats
        if (TryGetComponent<PlayerShooting>(out var player))
        {
            _statsProvider = player.PlayerStats;
            _damage = 0; // no melee damage for now
            return;
        }

        Debug.LogError($"{gameObject.name} has no stats for TakeDamage!");
    }

    /// <summary>
    /// Apply damage to this object
    /// </summary>
    /// <param name="damage">Amount of damage to apply</param>
    public void ApplyDamage(int damage)
    {
        if (_statsProvider == null) return;

        _statsProvider.Health -= damage;
        //Debug.Log($"{gameObject.name} took {damage} damage. Remaining HP: {_statsProvider.Health}");

        if (_statsProvider.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ---------------------------
        // 1️⃣ Bullet hits Monster
        // ---------------------------
        if (other.TryGetComponent<Bullet>(out var bullet) &&
            TryGetComponent<BasicMonster>(out var monster))
        {
            ApplyDamage(bullet.ProjectileStats.Damage);
            Destroy(other.gameObject); // destroy bullet
        }

        // ---------------------------
        // 2️⃣ Monster hits Player
        // ---------------------------
        if (TryGetComponent<PlayerShooting>(out var player) &&
            other.TryGetComponent<BasicMonster>(out var monsterOther))
        {
            // Player takes monster's damage
            ApplyDamage(monsterOther.MonsterStats.Damage);

            // Optionally destroy monster after hitting player
            Destroy(other.gameObject);
        }
    }


}
