using Assets.Interface;
using Assets.Scripts;
using Assets.Scripts.Monster;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [Tooltip("Optional: assign explicitly; otherwise auto-found in children.")]
    [SerializeField] private TMP_Text healthText;

    private IHealth statsProvider;

    private void Awake()
    {
        // 1) Find the text if not assigned
        if (healthText == null)
            healthText = GetComponentInChildren<TMP_Text>(true);

        if (healthText == null)
        {
            Debug.LogError($"[HealthUI] No TMP_Text found under {name}. Add a TextMeshPro(Text) child.");
            enabled = false;
            return;
        }

        // 2) Find the health source (Monster or Player)
        var monster = GetComponentInParent<BasicMonster>();
        if (monster != null)
        {
            statsProvider = monster.MonsterStats;   // Monsters implements IHealth (model)
        }
        else
        {
            var player = GetComponentInParent<PlayerShooting>();
            if (player != null)
                statsProvider = player.PlayerStats; // Player model implements IHealth
        }

        if (statsProvider == null)
        {
            Debug.LogError($"[HealthUI] No health provider found on parents of {name}.");
            enabled = false;
        }
    }

    private void Update()
    {
        // 3) Display current health each frame
        healthText.text = statsProvider.Health.ToString();
    }
}
