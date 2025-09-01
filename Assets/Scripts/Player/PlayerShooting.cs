using Assets.Models;
using UnityEngine;
using static Assets.Models.Enums;

namespace Assets.Scripts
{
    public class PlayerShooting : MonoBehaviour
    {
        public Player PlayerStats { get; set;  }

        [Header("Assign your Bullet prefab here")]
        public GameObject bulletPrefab;
        public GameObject fireballPrefab;

        [Header("Assign your FirePoint transform here (child of Player)")]
        public Transform firePoint;

        private float _nextBulletTime = 0f;
        private float _nextFireballTime = 0f;

        private void Awake()
        {
            PlayerStats = GameStateEngine.Instance.GetPlayerStats();
        }

        private void Update()
        {
            Projectiles bulletStats = GameStateEngine.Instance.GetProjectileStats(ProjectileType.Bullet);
            Projectiles fireballStats = GameStateEngine.Instance.GetProjectileStats(ProjectileType.Fireball);

            // Bullets
            if (Time.time >= _nextBulletTime && MonsterFactory.Instance.HasMonsters())
            {
                for (int i = 0; i < bulletStats.Count; i++)
                    Shoot(bulletPrefab, bulletStats, i);

                _nextBulletTime = (float)(Time.time + bulletStats.Rate);
            }

            // Fireballs
            if (Time.time >= _nextFireballTime && MonsterFactory.Instance.HasMonsters())
            {
                for (int i = 0; i < fireballStats.Count; i++)
                    Shoot(fireballPrefab, fireballStats, i);

                _nextFireballTime = (float)(Time.time + fireballStats.Rate);
            }


        }

        private void Shoot(GameObject perfab, Projectiles Stats, int Index)
        {
            if (bulletPrefab == null || firePoint == null)
            {
                Debug.LogError("BulletPrefab or FirePoint not assigned in PlayerShooting!");
                return;
            }

            float spacing = 1f; // distance between bullets
            Vector3 offset = (Index - (Stats.Count - 1) / 2f) * spacing * firePoint.right;
            _ = Instantiate(perfab, firePoint.position + offset, firePoint.rotation);
        }

    }
}
