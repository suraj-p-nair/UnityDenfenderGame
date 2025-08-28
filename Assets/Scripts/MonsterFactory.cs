using Assets.Models;
using Assets.Scripts.Monster;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

        // Track total remaining monsters in current round
        public int RemainingMonsters { get; private set; }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        public void SpawnWave(Monsters stats)
        {
            Monsters waveStats = new Monsters(stats); // copy to avoid modifying base stats
            RemainingMonsters = waveStats.Count;
            StartCoroutine(SpawnWaveCoroutine(waveStats));
        }

        private IEnumerator SpawnWaveCoroutine(Monsters waveStats)
        {
            while (waveStats.Count > 0)
            {
                SpawnMonsterAtRandomEdge();
                waveStats.Count--;
                yield return new WaitForSeconds(1f / waveStats.Rate);
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

        // ---------------- Tracking ----------------

        public void RegisterMonster(BasicMonster monster)
        {
            if (!_activeMonsters.Contains(monster))
                _activeMonsters.Add(monster);
        }

        public void UnregisterMonster(BasicMonster monster)
        {
            if (_activeMonsters.Contains(monster))
            {
                _activeMonsters.Remove(monster);
                RemainingMonsters--; // decrement as monsters die
            }
        }

        public bool HasMonsters() => _activeMonsters.Count > 0;

        public List<BasicMonster> GetAllMonsters() => _activeMonsters;
        public BasicMonster GetNearestMonster(Vector3 position)
        {
            if (_activeMonsters.Count == 0) return null;

            return _activeMonsters
                .OrderBy(m => Vector3.Distance(position, m.transform.position))
                .FirstOrDefault();
        }

    }
}
