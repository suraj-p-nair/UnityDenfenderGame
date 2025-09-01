using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Models.Enums;
namespace Assets.Scripts
{
    public class GameStateEngine : MonoBehaviour
    {
        public Projectiles BaseBulletStats = new(damage: 10, rate: 1, speed: 3, count: 1) ;
        public Projectiles BaseFireballStats = new(damage: 20, rate: 2, speed: 1, count: 0) ;
        public Monsters BaseMonsterStats = new(health: 10,damage: 10,rate: 1,speed: 2, count: 10);
        public Monsters BaseBossStats = new(health: 100,damage: 100,rate: 1,speed: 0.5f, count: 1);
        public Player BasePlayerStats = new(health: 100);
        public static GameStateEngine Instance { get; private set; }
        public Dictionary<ProjectileType, Projectiles> CurrentProjectileStats;
        public Dictionary<MonsterType, Monsters> CurrentMonsterStats;
        public Player CurrentPlayerStats;

        public int CurrentRound { get; set; } = 1;
        public bool IsRoundActive { get; set; } = false;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            CurrentProjectileStats = new Dictionary<ProjectileType, Projectiles>
            {
                { ProjectileType.Bullet, BaseBulletStats },
                { ProjectileType.Fireball, BaseFireballStats }
            };
            CurrentMonsterStats = new Dictionary<MonsterType, Monsters>
            {
                { MonsterType.Basic, BaseMonsterStats },
                { MonsterType.Boss, BaseBossStats }
            };
            CurrentPlayerStats = new(BasePlayerStats);
        }

        public void UpgradeMonsterStatus()
        {
            CurrentMonsterStats[MonsterType.Basic].Health += 5;
            CurrentMonsterStats[MonsterType.Basic].Count += 2;
            CurrentMonsterStats[MonsterType.Basic].Damage += 2;
            BaseMonsterStats.Health += 5;
            BaseMonsterStats.Count += 2;
            BaseMonsterStats.Damage += 2;
        }
        public Projectiles GetProjectileStats(ProjectileType type) => CurrentProjectileStats[type];
        public Monsters GetMonsterStats(MonsterType type) => CurrentMonsterStats[type];
        public Player GetPlayerStats() => CurrentPlayerStats;

        public void ApplyUpgrade(Upgrade upgrade)
        {
            if (upgrade.Target == "Player")
            {
                if (upgrade.Type == UpgradeType.Health)
                {
                    double oldMax = BasePlayerStats.Health;
                    double oldCurrent = CurrentPlayerStats.Health;

                    // Get multiplier based on rarity
                    float multiplier = UpgradeLibrary.GetPlayerHealthMultiplier(upgrade.Rarity);

                    // Increase max health
                    BasePlayerStats.Health *= multiplier;

                    // Keep same ratio for current HP
                    double ratio = oldCurrent / oldMax;
                    CurrentPlayerStats.Health = BasePlayerStats.Health * ratio;
                }
            }
            else if (upgrade.Target.StartsWith("Projectile"))
            {
                var split = upgrade.Target.Split(':');
                var projectile = (ProjectileType)Enum.Parse(typeof(ProjectileType), split[1]);
                var stats = CurrentProjectileStats[projectile];

                switch (upgrade.Type)
                {
                    case UpgradeType.Damage:
                        stats.Damage *= UpgradeLibrary.GetDamageMultiplier(projectile, upgrade.Rarity);
                        break;

                    case UpgradeType.Count:
                        stats.Count += UpgradeLibrary.GetCountIncrement(projectile, upgrade.Rarity);
                        break;

                    case UpgradeType.Speed:
                        stats.Speed *= UpgradeLibrary.GetSpeedMultiplier(projectile, upgrade.Rarity);
                        break;

                    case UpgradeType.Rate:
                        stats.Rate *= UpgradeLibrary.GetRateMultiplier(projectile, upgrade.Rarity);
                        break;
                }
            }

            Debug.Log($"Applied {upgrade.Type} ({upgrade.Rarity}) to {upgrade.Target}");
        }


    }
}
