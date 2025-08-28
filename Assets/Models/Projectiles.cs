using Assets.Interface;
public class Projectiles : IStats
{
    public int Damage { get; set; }
    public int Rate { get; set; }
    public int Speed { get; set; }
    public Projectiles(int damage, int rate, int speed)
    {
        Damage = damage;
        Rate = rate;
        Speed = speed;
    }
    public Projectiles(Projectiles other) : this(other.Damage, other.Rate, other.Speed) { }
}
