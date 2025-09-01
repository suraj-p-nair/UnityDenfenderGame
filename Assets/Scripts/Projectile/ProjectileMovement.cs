using UnityEngine;
using Assets.Models;
using Assets.Scripts;
using Assets.Scripts.Monster;

namespace Assets.Scripts.Projectile
{
    [RequireComponent(typeof(ProjectileScript))]  // make sure a Projectile is always present
    public class ProjectileMovement : MonoBehaviour
    {
        private float _speed;
        private Transform _target;
        private Vector3 _lastDirection;
        private ProjectileScript _projectile;

        private void Start()
        {
            // Get this projectile’s own stats
            _projectile = GetComponent<ProjectileScript>();
            _speed = _projectile.ProjectileStats.Speed;

            // Find nearest monster
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
            _lastDirection = direction;
            transform.Translate(_speed * Time.deltaTime * direction, Space.World);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
