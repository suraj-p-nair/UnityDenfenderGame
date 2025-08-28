using UnityEngine;
using static Assets.Models.Enums;

namespace Assets.Scripts.Monster
{
    public class MonsterMovement : MonoBehaviour
    {
        private int _speed;

        private void Start()
        {
            _speed = GameStateEngine.Instance.GetMonsterStats(MonsterType.Basic).Speed;
        }

        private void Update()
        {
            // Direction from current position → origin (0,0,0)
            Vector3 direction = (-transform.position).normalized;

            // Move toward origin
            transform.Translate(_speed * Time.deltaTime * direction, Space.World);
        }
    }
}
