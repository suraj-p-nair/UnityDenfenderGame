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
            try
            {
                // Spawn player at (0,0,0)
                _playerInstance = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
                _playerInstance.name = "Player";
                MonsterFactory.Instance.SpawnMonsterAtRandomEdge();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error instantiating prefab: {ex.Message}");
            }
        }
    }
}
