using Assets.Interface;
public class Projectiles : IStats
{
    public double Damage { get; set; }
    public double Rate { get; set; }
    public float Speed { get; set; }
    public int Count { get; set; }
    public Projectiles(double damage, double rate, float speed, int count)
    {
        Damage = damage;
        Rate = rate;
        Speed = speed;
        Count = count;
    }
    public Projectiles(Projectiles other) : this(other.Damage, other.Rate, other.Speed, other.Count) { }
}
