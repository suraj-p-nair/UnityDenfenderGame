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
        public Projectiles BaseProjectileStats = new(damage: 1, rate: 5, speed: 8) ;
        public Monsters BaseMonsterStats = new(10,10,1,4);
        public Player BasePlayerStats = new(100);
        public static GameStateEngine Instance { get; private set; }
        public Dictionary<ProjectileType, Projectiles> CurrentProjectileStats;
        public Dictionary<MonsterType, Monsters> CurrentMonsterStats;
        public Player CurrentPlayerStats;
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
                { ProjectileType.Bullet, BaseProjectileStats }
            };
            CurrentMonsterStats = new Dictionary<MonsterType, Monsters>
            {
                { MonsterType.Basic, BaseMonsterStats }
            };
            CurrentPlayerStats = BasePlayerStats;
        }

        public void UpgradeMonsterStatus()
        {
            BaseMonsterStats.Health += 5;
        }
        public Projectiles GetProjectileStats(ProjectileType type) => CurrentProjectileStats[type];
        public Monsters GetMonsterStats(MonsterType type) => CurrentMonsterStats[type];
        public Player GetPlayerStats() => CurrentPlayerStats;
    }
}
