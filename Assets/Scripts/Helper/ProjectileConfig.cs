using System.Collections.Generic;

public static class ProjectileConfig
{
    public static Dictionary<ProjectileType, (int damage, float speed, float fireRate)> projectiles =
        new()
        {
            { ProjectileType.Bullet, (damage: 10, speed: 3f, fireRate: 0.5f) },
            { ProjectileType.Fireball, (damage: 25, speed: 2f, fireRate: 1f) }
        };
}

public enum ProjectileType
{
    Bullet,
    Fireball,
    Arrow
}
