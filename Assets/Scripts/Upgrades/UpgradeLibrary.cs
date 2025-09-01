using Assets.Models;
using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Models.Enums;

public static class UpgradeLibrary
{
    // -------------------- Weighted Random Selection --------------------
    public static List<Upgrade> GetRandomUpgrades(int count)
    {
        var chosen = new List<Upgrade>();

        for (int i = 0; i < count; i++)
        {
            // 1) Roll upgrade type first
            UpgradeType type = RollUpgradeType();

            // 2) Roll rarity for that upgrade
            UpgradeRarity rarity = RollUpgradeRarity();

            // 3) Generate upgrade dynamically
            Upgrade upgrade = GenerateUpgrade(type, rarity);
            if (upgrade != null)
                chosen.Add(upgrade);
        }

        return chosen;
    }

    // -------------------- Upgrade Generation --------------------
    private static Upgrade GenerateUpgrade(UpgradeType type, UpgradeRarity rarity)
    {
        switch (type)
        {
            case UpgradeType.Health:
                return CreatePlayerHealthUpgrade(rarity);

            case UpgradeType.Damage:
                ProjectileType damageProj = RollProjectileForDamage();
                return CreateProjectileDamageUpgrade(damageProj, rarity);

            case UpgradeType.Speed:
                ProjectileType speedProj = RollProjectileForSpeed();
                return CreateProjectileSpeedUpgrade(speedProj, rarity);

            case UpgradeType.Rate:
                ProjectileType rateProj = RollProjectileForRate();
                return CreateProjectileRateUpgrade(rateProj, rarity);

            case UpgradeType.Count:
                ProjectileType countProj = RollProjectileForCount();
                return CreateProjectileCountUpgrade(countProj, rarity);

            default:
                return null;
        }
    }

    // -------------------- Upgrade Type Roll --------------------
    private static UpgradeType RollUpgradeType()
    {
        // Only include meaningful upgrade types
        var types = Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>().ToList();
        return types[UnityEngine.Random.Range(0, types.Count)];
    }

    private static UpgradeRarity RollUpgradeRarity()
    {
        // Weighted probability example: adjust as needed
        Dictionary<UpgradeRarity, int> weights = new()
        {
            { UpgradeRarity.Common, 0 },
            { UpgradeRarity.Rare, 0 },
            { UpgradeRarity.Legendary, 0 },
            { UpgradeRarity.Mythical, 100 }
        };

        int totalWeight = 0;
        foreach (var w in weights.Values) totalWeight += w;

        int roll = UnityEngine.Random.Range(0, totalWeight);
        int cumulative = 0;

        foreach (var kv in weights)
        {
            cumulative += kv.Value;
            if (roll < cumulative)
                return kv.Key;
        }

        return UpgradeRarity.Common;
    }

    // -------------------- Projectile Selection --------------------
    private static ProjectileType RollProjectileForDamage() => RollProjectileWithMeaningfulMultiplier(GetDamageMultiplier);
    private static ProjectileType RollProjectileForSpeed() => RollProjectileWithMeaningfulMultiplier(GetSpeedMultiplier);
    private static ProjectileType RollProjectileForRate() => RollProjectileWithMeaningfulMultiplier(GetRateMultiplier);
    private static ProjectileType RollProjectileForCount() => RollProjectileWithMeaningfulCount();

    private static ProjectileType RollProjectileWithMeaningfulMultiplier(Func<ProjectileType, UpgradeRarity, float> multiplierFunc)
    {
        var valid = new List<ProjectileType>();

        foreach (ProjectileType proj in Enum.GetValues(typeof(ProjectileType)))
        {
            foreach (UpgradeRarity rarity in Enum.GetValues(typeof(UpgradeRarity)))
            {
                if (multiplierFunc(proj, rarity) != 1f)
                {
                    valid.Add(proj);
                    break;
                }
            }
        }

        return valid[UnityEngine.Random.Range(0, valid.Count)];
    }

    private static ProjectileType RollProjectileWithMeaningfulCount()
    {
        var valid = new List<ProjectileType>();
        foreach (ProjectileType proj in Enum.GetValues(typeof(ProjectileType)))
        {
            foreach (UpgradeRarity rarity in Enum.GetValues(typeof(UpgradeRarity)))
            {
                if (GetCountIncrement(proj, rarity) != 0)
                {
                    valid.Add(proj);
                    break;
                }
            }
        }

        return valid[UnityEngine.Random.Range(0, valid.Count)];
    }

    // -------------------- Upgrade Creation --------------------
    private static Upgrade CreatePlayerHealthUpgrade(UpgradeRarity rarity)
    {
        var current = GameStateEngine.Instance.CurrentPlayerStats;
        var baseStats = GameStateEngine.Instance.BasePlayerStats;

        float multiplier = GetPlayerHealthMultiplier(rarity);
        double newCurrent = Math.Round(current.Health * multiplier, 2);
        double newBase = Math.Round(baseStats.Health * multiplier, 2);

        return new Upgrade
        {
            Type = UpgradeType.Health,
            Target = "Player",
            Rarity = rarity,
            Name = $"{current.Health}/{baseStats.Health} -> {newCurrent}/{newBase}",
            Description = $"+{Math.Round((multiplier - 1f) * 100, 0)}% HP"
        };
    }

    private static Upgrade CreateProjectileDamageUpgrade(ProjectileType proj, UpgradeRarity rarity)
    {
        var stats = GameStateEngine.Instance.GetProjectileStats(proj);
        float multiplier = GetDamageMultiplier(proj, rarity);
        double newValue = Math.Round(stats.Damage * multiplier, 2);

        return new Upgrade
        {
            Type = UpgradeType.Damage,
            Target = $"Projectile:{proj}",
            Rarity = rarity,
            Name = $"{stats.Damage} -> {newValue}",
            Description = $"+{Math.Round((multiplier - 1f) * 100, 0)}% {proj} Damage"
        };
    }

    private static Upgrade CreateProjectileSpeedUpgrade(ProjectileType proj, UpgradeRarity rarity)
    {
        var stats = GameStateEngine.Instance.GetProjectileStats(proj);
        float multiplier = GetSpeedMultiplier(proj, rarity);
        double newValue = Math.Round(stats.Speed * multiplier, 2);

        return new Upgrade
        {
            Type = UpgradeType.Speed,
            Target = $"Projectile:{proj}",
            Rarity = rarity,
            Name = $"{stats.Speed} -> {newValue}",
            Description = $"+{Math.Round((multiplier - 1f) * 100, 0)}% {proj} Speed"
        };
    }

    private static Upgrade CreateProjectileRateUpgrade(ProjectileType proj, UpgradeRarity rarity)
    {
        var stats = GameStateEngine.Instance.GetProjectileStats(proj);
        float multiplier = GetRateMultiplier(proj, rarity);
        double newValue = Math.Round(stats.Rate * multiplier, 2);

        return new Upgrade
        {
            Type = UpgradeType.Rate,
            Target = $"Projectile:{proj}",
            Rarity = rarity,
            Name = $"{stats.Rate} -> {newValue:F2}",
            Description = $"-{Math.Round((1f - multiplier) * 100, 0)}% {proj} Fire Rate"
        };
    }

    private static Upgrade CreateProjectileCountUpgrade(ProjectileType proj, UpgradeRarity rarity)
    {
        var stats = GameStateEngine.Instance.GetProjectileStats(proj);
        int increment = GetCountIncrement(proj, rarity);

        return new Upgrade
        {
            Type = UpgradeType.Count,
            Target = $"Projectile:{proj}",
            Rarity = rarity,
            Name = $"{stats.Count} -> {stats.Count + increment}",
            Description = $"+{increment} {proj} Count"
        };
    }

    // -------------------- Multipliers --------------------
    public static float GetDamageMultiplier(ProjectileType type, UpgradeRarity rarity)
    {
        return (type, rarity) switch
        {
            (ProjectileType.Bullet, UpgradeRarity.Common) => 1.10f,
            (ProjectileType.Bullet, UpgradeRarity.Rare) => 1.20f,
            (ProjectileType.Bullet, UpgradeRarity.Legendary) => 1.30f,
            (ProjectileType.Bullet, UpgradeRarity.Mythical) => 1.50f,

            (ProjectileType.Fireball, UpgradeRarity.Common) => 1.25f,
            (ProjectileType.Fireball, UpgradeRarity.Rare) => 1.50f,
            (ProjectileType.Fireball, UpgradeRarity.Legendary) => 1.75f,
            (ProjectileType.Fireball, UpgradeRarity.Mythical) => 2.00f,

            _ => 1f
        };
    }

    public static float GetSpeedMultiplier(ProjectileType type, UpgradeRarity rarity)
    {
        return (type, rarity) switch
        {
            (ProjectileType.Bullet, UpgradeRarity.Common) => 1.05f,
            (ProjectileType.Bullet, UpgradeRarity.Rare) => 1.10f,
            (ProjectileType.Bullet, UpgradeRarity.Legendary) => 1.15f,
            (ProjectileType.Bullet, UpgradeRarity.Mythical) => 1.20f,

            (ProjectileType.Fireball, UpgradeRarity.Common) => 1.10f,
            (ProjectileType.Fireball, UpgradeRarity.Rare) => 1.15f,
            (ProjectileType.Fireball, UpgradeRarity.Legendary) => 1.20f,
            (ProjectileType.Fireball, UpgradeRarity.Mythical) => 1.25f,

            _ => 1f
        };
    }

    public static float GetRateMultiplier(ProjectileType type, UpgradeRarity rarity)
    {
        return (type, rarity) switch
        {
            (ProjectileType.Bullet, UpgradeRarity.Common) => 0.95f,
            (ProjectileType.Bullet, UpgradeRarity.Rare) => 0.90f,
            (ProjectileType.Bullet, UpgradeRarity.Legendary) => 0.85f,
            (ProjectileType.Bullet, UpgradeRarity.Mythical) => 0.75f,

            (ProjectileType.Fireball, UpgradeRarity.Common) => 0.90f,
            (ProjectileType.Fireball, UpgradeRarity.Rare) => 0.85f,
            (ProjectileType.Fireball, UpgradeRarity.Legendary) => 0.80f,
            (ProjectileType.Fireball, UpgradeRarity.Mythical) => 0.75f,

            _ => 1f
        };
    }

    public static int GetCountIncrement(ProjectileType type, UpgradeRarity rarity)
    {
        return (type, rarity) switch
        {
            (ProjectileType.Bullet, UpgradeRarity.Rare) => 1,
            (ProjectileType.Bullet, UpgradeRarity.Legendary) => 1,
            (ProjectileType.Bullet, UpgradeRarity.Mythical) => 2,

            (ProjectileType.Fireball, UpgradeRarity.Legendary) => 1,
            (ProjectileType.Fireball, UpgradeRarity.Mythical) => 1,

            _ => 0
        };
    }

    public static float GetPlayerHealthMultiplier(UpgradeRarity rarity)
    {
        return rarity switch
        {
            UpgradeRarity.Common => 1.10f,
            UpgradeRarity.Rare => 1.25f,
            UpgradeRarity.Legendary => 1.50f,
            UpgradeRarity.Mythical => 2.00f,
            _ => 1f
        };
    }
}
