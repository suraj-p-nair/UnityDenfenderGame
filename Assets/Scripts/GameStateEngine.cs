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
        public Projectiles BaseProjectileStats = new(damage: 10, rate: 2, speed: 2) ;
        public Monsters BaseMonsterStats = new(health: 10,damage: 10,rate: 1,speed: 2, count: 10);
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
            CurrentMonsterStats[MonsterType.Basic].Health += 5;
            CurrentMonsterStats[MonsterType.Basic].Count += 2;
            CurrentMonsterStats[MonsterType.Basic].Damage += 2;
        }
        public Projectiles GetProjectileStats(ProjectileType type) => CurrentProjectileStats[type];
        public Monsters GetMonsterStats(MonsterType type) => CurrentMonsterStats[type];
        public Player GetPlayerStats() => CurrentPlayerStats;
    }
}
