using Assets.Interface;
namespace Assets.Models
{
    public class Monsters : IHealth, IStats
    {
        public double Health { get; set; }
        public double Damage { get; set; }
        public double Rate { get; set; }
        public float Speed { get; set; }
        public int Count { get; set; }
        public Monsters(double health, double damage, double rate, float speed, int count)
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
