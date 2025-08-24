using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.EntityScripts
{
    
    internal class Shooter: MonoBehaviour, IHasHealth
    {
        private int _health = 10;
        public int Health => _health;
        void Update()
        {
            Debug.Log($"Shooter health: {_health}");
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Monsters>(out var monster))
            {
                TakeDamage(monster._damage);
                Destroy(collision.gameObject);
            }
        }
        public void TakeDamage(int dmg)
        {
            _health -= dmg;
            if (_health <= 0)
                Destroy(gameObject);
        }
    }
}
