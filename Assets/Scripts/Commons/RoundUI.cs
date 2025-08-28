using TMPro;
using UnityEngine;

namespace Assets.Scripts.Commons
{
    public class RoundUI : MonoBehaviour
    {
        [Header("UI References")]
        public TMP_Text RoundText;
        public TMP_Text MonsterCountText;

        private void Update()
        {
            if (GameStateEngine.Instance == null || MonsterFactory.Instance == null)
                return;

            // Round number
            RoundText.text = $"Round: {GameStateEngine.Instance.CurrentRound}";

            // Remaining monsters
            int remaining = MonsterFactory.Instance.RemainingMonsters;
            MonsterCountText.text = $"Monsters: {remaining}";
        }
    }
}
