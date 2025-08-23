using System.Collections.Generic;

public class MonsterStats
{
    public int health;
    public float speed;
    public int count;
    public float rate;

    public MonsterStats(int health, float speed, int count, float rate)
    {
        this.health = health;
        this.speed = speed;
        this.count = count;
        this.rate = rate;
    }
}

public static class MonsterConfig
{
    public static Dictionary<MonsterType, MonsterStats> monsters = new();

    public static void Initialize()
    {
        monsters[MonsterType.Square] = new MonsterStats(10, 2f, 10, 0.5f);
        monsters[MonsterType.Circle] = new MonsterStats(15, 1.5f, 5, 1f);
        monsters[MonsterType.Boss] = new MonsterStats(100, 1f, 1, 0f);
    }
}

public enum MonsterType
{
    Square,
    Circle,
    Boss
}
