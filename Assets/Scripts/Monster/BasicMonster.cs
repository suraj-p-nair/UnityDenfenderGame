using Assets.Models;
using UnityEngine;
using static Assets.Models.Enums;
namespace Assets.Scripts.Monster
{
    public class BasicMonster : MonoBehaviour
    {
        public Monsters MonsterStats { get; private set; }
        private void Awake()
        {
            Monsters stats = GameStateEngine.Instance.GetMonsterStats(MonsterType.Basic);
            MonsterStats = new Monsters(stats);
            Debug.Log($"Monster Health: {MonsterStats.Health}, Damage: {MonsterStats.Damage}, Rate: {MonsterStats.Rate}, Speed: {MonsterStats.Speed}");
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
