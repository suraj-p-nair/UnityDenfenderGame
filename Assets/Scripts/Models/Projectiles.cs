using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public ProjectileType projectileType;
    public int damage;
    public float speed;
    public float fireRate;
    public Vector3 direction;

    // Instance initialization
    public virtual void Initialize(ProjectileType type)
    {
        projectileType = type;

        if (ProjectileConfig.projectiles.TryGetValue(type, out var stats))
        {
            damage = stats.damage;
            speed = stats.speed;
            fireRate = stats.fireRate;
        }
        else
        {
            Debug.LogWarning($"ProjectileConfig missing entry for {type}");
        }
    }

    // Static initialization for prefabs
    public static void InitializePrefab(GameObject prefab, ProjectileType type)
    {
        if (prefab == null) return;

        var projectile = prefab.GetComponent<Projectiles>();
        if (projectile == null)
        {
            Debug.LogWarning($"Prefab {prefab.name} does not have a Projectiles component!");
            return;
        }

        projectile.Initialize(type);
    }
}
