using Assets.Interface;
namespace Assets.Models
{
    public class Monsters : IHealth, IStats
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Rate { get; set; }
        public int Speed { get; set; }
        public int Count { get; set; }
        public Monsters(int health, int damage, int rate, int speed, int count)
        {
            Health = health;
            Damage = damage;
            Rate = rate;
            Speed = speed;
            Count = count;
        }
        public Monsters(Monsters other) : this(other.Health, other.Damage, other.Rate, other.Speed, other.Count) { }
    }
}
