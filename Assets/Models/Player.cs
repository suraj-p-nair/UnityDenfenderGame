using Assets.Interface;
namespace Assets.Models
{
    public class Player : IHealth
    {
        public int Health { get; set; }
        public Player(int health)
        {
            Health = health;
        }
        public Player(Player other) : this(other.Health) {}
    }
}
