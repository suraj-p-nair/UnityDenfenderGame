using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameObject monsterPrefab; // prefab with SquareMonster attached
    public GameObject bossPrefab; // prefab with SquareMonster attached
    public GameObject bulletPrefab;  // prefab with Projectiles attached

    void Start()
    {
        // Initialize projectiles
        Projectiles.InitializePrefab(bulletPrefab, ProjectileType.Bullet);

        // Spawn monsters
        MonsterFactory.Instance.SpawnMonsters(MonsterType.Square, monsterPrefab);
        MonsterFactory.Instance.SpawnMonsters(MonsterType.Boss, bossPrefab);
    }
}
