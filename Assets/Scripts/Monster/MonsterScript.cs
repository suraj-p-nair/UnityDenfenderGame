using Assets.Models;
using UnityEngine;
using static Assets.Models.Enums;

namespace Assets.Scripts.Monster
{
    public class MonsterScript : MonoBehaviour
    {
        [SerializeField] private MonsterType type; // assign in prefab (Basic, Boss, etc.)
        public Monsters MonsterStats { get; private set; }

        private void Awake()
        {
            // fetch stats for the given type
            Monsters stats = GameStateEngine.Instance.GetMonsterStats(type);
            MonsterStats = new Monsters(stats);

            gameObject.name = $"{type}_{Time.frameCount}";
        }

        private void OnEnable()
        {
            MonsterFactory.Instance.RegisterMonster(this);
        }

        private void OnDisable()
        {
            if (MonsterFactory.Instance != null)
                MonsterFactory.Instance.UnregisterMonster(this);
        }
    }
}
