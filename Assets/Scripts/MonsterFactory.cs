using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Monster;
using Assets.Models;
using static Assets.Models.Enums;

namespace Assets.Scripts
{
    public class MonsterFactory : MonoBehaviour
    {
        private static MonsterFactory _instance;
        public static MonsterFactory Instance => _instance;

        [Header("Monster Prefab")]
        public GameObject MonsterPrefab;

        private readonly List<BasicMonster> _activeMonsters = new();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        private void Start()
        {
            if (MonsterPrefab != null)
                StartCoroutine(SpawnMonstersCoroutine());
            else
                Debug.LogError("MonsterPrefab not assigned in MonsterFactory!");
        }

        private IEnumerator SpawnMonstersCoroutine()
        {
            // Get spawn rate from monster stats
            Monsters stats = GameStateEngine.Instance.GetMonsterStats(MonsterType.Basic);

            while (true)
            {
                SpawnMonsterAtRandomEdge();

                // Use monster "Rate" as interval
                yield return new WaitForSeconds(1f / stats.Rate);
            }
        }

        public GameObject SpawnMonsterAtRandomEdge()
        {
            if (MonsterPrefab == null) return null;

            Camera cam = Camera.main;
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            Vector3 spawnPos = new Vector3(
                Random.Range(-camWidth / 2, camWidth / 2),
                Random.Range(-camHeight / 2, camHeight / 2),
                0f
            );

            GameObject monster = Instantiate(MonsterPrefab, spawnPos, Quaternion.identity);
            monster.name = $"Monster_{Time.frameCount}";
            return monster;
        }

        // Tracking ---------------------------------------------------

        public void RegisterMonster(BasicMonster monster)
        {
            if (!_activeMonsters.Contains(monster))
                _activeMonsters.Add(monster);
        }

        public void UnregisterMonster(BasicMonster monster)
        {
            _activeMonsters.Remove(monster);

            if (_activeMonsters.Count == 0)
            {
                Debug.Log("All monsters defeated! Player wins!");
                //GameLogicEngine.Instance.OnAllMonstersDefeated();
            }
        }

        public bool HasMonsters() => _activeMonsters.Count > 0;

        public BasicMonster GetNearestMonster(Vector3 position)
        {
            if (_activeMonsters.Count == 0) return null;
            return _activeMonsters
                .OrderBy(m => Vector3.Distance(position, m.transform.position))
                .FirstOrDefault();
        }

        public List<BasicMonster> GetAllMonsters() => _activeMonsters;
    }
}
