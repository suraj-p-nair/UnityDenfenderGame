using UnityEngine;
using System.Collections;

public class SpawnMonsters : MonoBehaviour
{
    public Transform P0;                 // left endpoint
    public Transform P1;                 // right endpoint
    public GameObject monsterPrefab;     // drag your monster prefab here
    private float spawnRate = 0.5f;         // seconds between spawns

    private void Start()
    {
        // start spawning monsters every spawnRate seconds
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnMonster()
    {
        Vector3 spawnPos = Vector3.Lerp(P0.position, P1.position, Random.value);

        if (monsterPrefab == null || P0 == null || P1 == null)
        {
            Debug.LogWarning("Spawner missing references!");
            return;
        }

       

        // Create monster at that position
        Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
    }
}
