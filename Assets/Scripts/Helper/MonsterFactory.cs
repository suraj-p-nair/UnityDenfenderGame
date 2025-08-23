using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterFactory : MonoBehaviour
{
    public static MonsterFactory Instance { get; private set; }

    public Transform P0;
    public Transform P1;

    private List<GameObject> activeMonsters = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        MonsterConfig.Initialize();
    }

    public bool AnyMonstersExist() => activeMonsters.Count > 0;

    public Transform GetClosestMonster(Vector3 position)
    {
        if (activeMonsters.Count == 0) return null;

        GameObject closest = null;
        float minDistance = float.MaxValue;

        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;
            float dist = Vector3.Distance(position, monster.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = monster;
            }
        }

        return closest?.transform;
    }

    public void SpawnMonsters(MonsterType type, GameObject monsterPrefab)
    {
        if (!MonsterConfig.monsters.TryGetValue(type, out var stats))
        {
            Debug.LogWarning($"Monster config missing for {type}");
            return;
        }

        StartCoroutine(SpawnLoop(monsterPrefab, stats, type));
    }

    private IEnumerator SpawnLoop(GameObject prefab, MonsterStats stats, MonsterType type)
    {
        for (int i = 0; i < stats.count; i++)
        {
            SpawnMonster(prefab, stats, type);
            yield return new WaitForSeconds(stats.rate);
        }
    }

    private void SpawnMonster(GameObject prefab, MonsterStats stats, MonsterType type)
    {
        Vector3 spawnPos = Vector3.Lerp(P0.position, P1.position, Random.value);
        GameObject monsterGO = Instantiate(prefab, spawnPos, Quaternion.identity);

        var monsterComp = monsterGO.GetComponent<Monsters>();
        if (monsterComp != null)
        {
            monsterComp.Initialize(type, stats.health, stats.speed);
            activeMonsters.Add(monsterGO);

            monsterComp.OnMonsterDestroyed += () => activeMonsters.Remove(monsterGO);
        }
        else
        {
            Debug.LogWarning($"Prefab {prefab.name} does not have a Monsters component!");
        }
    }
}
