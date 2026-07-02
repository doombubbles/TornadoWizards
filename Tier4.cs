using System;
using System.Linq;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using PathsPlusPlus;

namespace TornadoWizards;

public class ElectricOverload : UpgradePlusPlus<TornadoWizardPath>
{
    public override int Tier => 4;
    public override string Portrait => "Wizard4";

    public override int Cost => TornadoWizardsMod.BallLightning
        ? Game.instance.model.GetUpgrade(UpgradeType.BallLightning).cost
        : 7000;

    public override string DisplayName => TornadoWizardsMod.BallLightning
        ? "[Ball Lightning]"
        : base.DisplayName;

    public override string Description => TornadoWizardsMod.BallLightning
        ? "[Ball Lightning Description]"
        : "Supercharges the Lightning attack, increasing damage, speed, and splitting.";

    public override string Icon =>
        TornadoWizardsMod.BallLightning ? VanillaSprites.BallLightningUpgradeIcon : base.Icon;

    public override void ApplyUpgrade(TowerModel towerModel, int tier)
    {
        if (TornadoWizardsMod.BallLightning)
        {
            var druid = Game.instance.model.GetTower(TowerType.Druid, Math.Min(tier, 5));
            var ballLightning = druid.GetAttackModel().weapons
                .First(w => w.name == "WeaponModel_BallLightning")
                .Duplicate();

            ballLightning.animation = 1;
            ballLightning.projectile.GetDamageModel().immuneBloonProperties &= ~BloonProperties.Purple;

            if (towerModel.appliedUpgrades.Contains(UpgradeType.MonkeySense))
            {
                ballLightning.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
            }

            towerModel.GetAttackModel().AddWeapon(ballLightning);
        }
        else
        {
            var weapon = towerModel.GetAttackModel().weapons
                .FirstOrDefault(w => w.name == "WeaponModel_Lightning" || w.modelName.Contains("Lightning"));
            if (weapon != null)
            {
                weapon.Rate /= 2;
                var lightning = weapon.projectile;
                lightning.GetDamageModel().damage += 4;
                lightning.GetBehavior<LightningModel>().splits++;
            }
        }

        if (IsHighestUpgrade(towerModel))
        {
            towerModel.display = towerModel.GetBehavior<DisplayModel>().display =
                Game.instance.model.GetTower(TowerType.Druid, 4, 0, 0).display;
        }
    }
}