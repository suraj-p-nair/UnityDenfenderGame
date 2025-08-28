using UnityEngine;
using static Assets.Models.Enums;
namespace Assets.Scripts.Projectile
{
    public class Bullet : MonoBehaviour
    {
        public Projectiles ProjectileStats { get; private set; }
        private void Awake()
        {
            gameObject.name = $"Bullet_{Time.frameCount}";
            Projectiles stats = GameStateEngine.Instance.GetProjectileStats(ProjectileType.Bullet);
            ProjectileStats = new Projectiles(stats);
            Destroy(gameObject, 2f);
        }
    }
}
