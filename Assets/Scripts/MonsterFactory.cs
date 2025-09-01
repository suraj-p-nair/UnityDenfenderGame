using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Monster;
namespace Assets.Scripts
{
    public class MonsterFactory : MonoBehaviour
    {
        private static MonsterFactory _instance;
        public static MonsterFactory Instance => _instance;

        [Header("Monster Prefab")]
        public GameObject MonsterPrefab;

        private readonly List<MonsterScript> _activeMonsters = new();

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
            Monsters waveStats = new Monsters(stats);
            RemainingMonsters = waveStats.Count;
            StartCoroutine(SpawnWaveCoroutine(waveStats));
        }

        private IEnumerator SpawnWaveCoroutine(Monsters waveStats)
        {
            while (waveStats.Count > 0)
            {
                SpawnMonsterAtRandomEdge();
                waveStats.Count--;
                yield return new WaitForSeconds((float)(1f / waveStats.Rate));
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

        public void RegisterMonster(MonsterScript monster)
        {
            if (!_activeMonsters.Contains(monster))
                _activeMonsters.Add(monster);
        }

        public void UnregisterMonster(MonsterScript monster)
        {
            if (_activeMonsters.Contains(monster))
            {
                _activeMonsters.Remove(monster);
                RemainingMonsters--; // decrement as monsters die
            }
        }

        public bool HasMonsters() => _activeMonsters.Count > 0;

        public List<MonsterScript> GetAllMonsters() => _activeMonsters;
        public MonsterScript GetNearestMonster(Vector3 position)
        {
            if (_activeMonsters.Count == 0) return null;

            return _activeMonsters
                .OrderBy(m => Vector3.Distance(position, m.transform.position))
                .FirstOrDefault();
        }


        [Header("Boss Prefab")]
        public GameObject BossPrefab;

        public GameObject SpawnBoss()
        {
            if (BossPrefab == null) return null;

            Camera cam = Camera.main;
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            Vector3 spawnPos = new Vector3(
                Random.Range(-camWidth / 2, camWidth / 2),
                Random.Range(-camHeight / 2, camHeight / 2),
                0f
            );

            GameObject boss = Instantiate(BossPrefab, spawnPos, Quaternion.identity);
            boss.name = $"Boss_{Time.frameCount}";

            RemainingMonsters++; // so round waits for boss to die
            return boss;
        }

    }
}
