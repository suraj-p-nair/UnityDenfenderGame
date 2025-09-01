using Assets.Interface;
namespace Assets.Models
{
    public class Player : IHealth
    {
        public double Health { get; set; }
        public int UpgradeCount { get; set; } = 2;
        public Player(double health)
        {
            Health = health;
        }
        public Player(Player other) : this(other.Health) {}
    }
}
