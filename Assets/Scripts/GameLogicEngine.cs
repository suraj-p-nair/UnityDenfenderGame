using Assets.Models;
using System.Collections;
using UnityEngine;
using static Assets.Models.Enums;

namespace Assets.Scripts
{
    public class GameLogicEngine : MonoBehaviour
    {
        [Header("Assign prefabs in Inspector")]
        public GameObject PlayerPrefab;

        private GameObject _playerInstance;

        private void Start()
        {
            // Spawn player
            try
            {
                _playerInstance = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
                _playerInstance.name = "Player";
                Debug.Log("Player instantiated successfully.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error instantiating prefab: {ex.Message}");
            }

            StartCoroutine(RoundLoop());
        }

        private IEnumerator RoundLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f); // small delay before round starts

                // Start round
                GameStateEngine.Instance.IsRoundActive = true;
                Debug.Log($"Round {GameStateEngine.Instance.CurrentRound} started!");

                // Spawn monsters
                Monsters stats = GameStateEngine.Instance.GetMonsterStats(MonsterType.Basic);
                MonsterFactory.Instance.SpawnWave(stats);

                // 🔹 Spawn boss every 5th round
                if (GameStateEngine.Instance.CurrentRound % 2 == 0)
                {
                    MonsterFactory.Instance.SpawnBoss();
                    Debug.Log("Boss spawned!");
                }

                // Wait until all monsters are dead AND wave fully spawned
                yield return new WaitUntil(() =>
                    MonsterFactory.Instance.RemainingMonsters == 0 &&
                    !MonsterFactory.Instance.HasMonsters()
                );

                // End round
                GameStateEngine.Instance.IsRoundActive = false;
                Debug.Log($"Round {GameStateEngine.Instance.CurrentRound} complete!");

                // Show upgrade screen
                yield return ShowUpgradeScreen();

                // Upgrade monster stats for next round
                GameStateEngine.Instance.UpgradeMonsterStatus();

                // Increment round
                GameStateEngine.Instance.CurrentRound++;

                yield return new WaitForSeconds(1f); // short delay before next round
            }
        }


        // Coroutine to show upgrades
        private IEnumerator ShowUpgradeScreen()
        {
            bool upgradeChosen = false;

            var randomUpgrades = UpgradeLibrary.GetRandomUpgrades(2);

            UpgradeManager upgradeManager = Object.FindFirstObjectByType<UpgradeManager>();
            upgradeManager.ShowUpgrades(randomUpgrades[0], randomUpgrades[1], (chosen) =>
            {
                GameStateEngine.Instance.ApplyUpgrade(chosen);
                upgradeChosen = true;
            });

            yield return new WaitUntil(() => upgradeChosen);
        }

    }
}
