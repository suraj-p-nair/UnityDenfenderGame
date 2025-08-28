using UnityEngine;
using Assets.Models;
using Assets.Scripts;
using Assets.Scripts.Monster;
using static Assets.Models.Enums;

namespace Assets.Scripts.Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {
        private float _speed;
        private Transform _target;
        private Vector3 _lastDirection; // stores the last movement direction

        private void Start()
        {
            _speed = GameStateEngine.Instance.GetProjectileStats(ProjectileType.Bullet).Speed;
            var monster = MonsterFactory.Instance.GetNearestMonster(transform.position);
            if (monster != null)
            {
                _target = monster.transform;
                _lastDirection = (_target.position - transform.position).normalized;
            }
            else
            {
                Destroy(gameObject);
            }
        }


        private void Update()
        {
            if (_target == null)
            {
                transform.Translate(_speed * Time.deltaTime * _lastDirection, Space.World);
                return;
            }
            Vector3 direction = (_target.position - transform.position).normalized;
            _lastDirection = direction; // update last direction
            transform.Translate(_speed * Time.deltaTime * direction, Space.World);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
