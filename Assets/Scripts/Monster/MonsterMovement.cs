using UnityEngine;

namespace Assets.Scripts.Monster
{
    [RequireComponent(typeof(MonsterScript))]  // ensures a Monster script exists
    public class MonsterMovement : MonoBehaviour
    {
        private float _speed;
        private MonsterScript _monster;

        private void Awake()
        {
            _monster = GetComponent<MonsterScript>();
        }

        private void Start()
        {
            _speed = (float)_monster.MonsterStats.Speed;
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

