using UnityEngine;
using static Assets.Models.Enums;

public class Upgrade
{
    public UpgradeType Type;
    public string Name;
    public string Description;
    public UpgradeRarity Rarity;
    public string Target;

    // Convenience property for UI
    public Color RarityColor => Rarity switch
    {
        UpgradeRarity.Common => Color.white,
        UpgradeRarity.Rare => new Color(0.3f, 0.6f, 1f),   // blue-ish
        UpgradeRarity.Legendary => new Color(1f, 0.5f, 0f),     // orange
        UpgradeRarity.Mythical => new Color(0.7f, 0f, 1f),     // purple
        _ => Color.gray
    };
}
