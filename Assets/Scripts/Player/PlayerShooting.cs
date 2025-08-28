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

        [Header("Assign your FirePoint transform here (child of Player)")]
        public Transform firePoint;

        private float _nextFireTime = 0f;
        private void Awake()
        {
            Player stats = GameStateEngine.Instance.GetPlayerStats();
            PlayerStats = new Player(stats);
        }

        private void Update()
        {
            Projectiles bulletStats = GameStateEngine.Instance.GetProjectileStats(ProjectileType.Bullet);

            if (Time.time >= _nextFireTime)
            {
                if (MonsterFactory.Instance.HasMonsters())
                {
                    Shoot(bulletStats);
                    _nextFireTime = Time.time + 1f / bulletStats.Rate;
                }
            }

        }

        private void Shoot(Projectiles bulletStats)
        {
            if (bulletPrefab == null || firePoint == null)
            {
                Debug.LogError("BulletPrefab or FirePoint not assigned in PlayerShooting!");
                return;
            }
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log($"Bullet spawned with Damage={bulletStats.Damage}, Speed={bulletStats.Speed}, Rate={bulletStats.Rate}");
        }
    }
}
