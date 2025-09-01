using UnityEngine;
using static Assets.Models.Enums;

namespace Assets.Scripts.Projectile
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] private ProjectileType type; // assign in prefab
        public Projectiles ProjectileStats { get; private set; }

        private void Awake()
        {
            // get base stats from GameStateEngine
            var stats = GameStateEngine.Instance.GetProjectileStats(type);

            // make a copy so this instance has its own values
            ProjectileStats = new Projectiles(stats);

            
            Destroy(gameObject, 6/ProjectileStats.Speed);

            // rename for debugging clarity
            gameObject.name = $"{type}_{Time.frameCount}";
        }
    }
}
