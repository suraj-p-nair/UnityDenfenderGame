using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Enums
    {
        public enum ProjectileType
        {
            Bullet,
            Fireball
        }
        public enum MonsterType
        {
            Basic,
            Boss
        }
        public enum UpgradeType
        {
            Health,
            Damage,
            Speed,
            Count,
            Rate
        }

        public enum UpgradeRarity
        {
            Common,
            Rare,
            Legendary,
            Mythical
        }
    }
}
