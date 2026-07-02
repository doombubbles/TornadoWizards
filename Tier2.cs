using System;
using System.Linq;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using PathsPlusPlus;

namespace TornadoWizards;

public class LightningBolt : UpgradePlusPlus<TornadoWizardPath>
{
    public override int Cost => 1020;
    public override int Tier => 2;
    public override string Portrait => "Wizard2";

    public override string Description => "Unleashes the power of lightning to zap many Bloons at once in a chain.";

    public override void ApplyUpgrade(TowerModel towerModel, int tier)
    {
        var druid = Game.instance.model.GetTower(TowerType.Druid, Math.Min(tier, 5));
        var lightning = druid.FindDescendant<WeaponModel>("Lightning").Duplicate();
        lightning.animation = 1;
        var damage = lightning.projectile.GetDamageModel();

        damage.immuneBloonProperties &= ~BloonProperties.Purple;

        if (towerModel.appliedUpgrades.Contains(UpgradeType.MonkeySense))
        {
            lightning.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
        }

        if (towerModel.appliedUpgrades.Contains(UpgradeType.ArcaneMastery))
        {
            damage.damage++;
            if (towerModel.appliedUpgrades.Contains(UpgradeType.ArcaneSpike))
            {
                var moabDamage = DamageModifierForTagModel.Create(new()
                {
                    damageAddative = 6,
                    tag = BloonTag.Moabs
                });
                lightning.projectile.AddBehavior(moabDamage);
                lightning.projectile.hasDamageModifiers = true;
                if (towerModel.appliedUpgrades.Contains(UpgradeType.Archmage))
                {
                    damage.damage += 7;
                    moabDamage.damageAddative = 24;

                    lightning.projectile.pierce++;
                    lightning.projectile.maxPierce++;

                    lightning.Rate /= 2;
                }
            }
        }

        if (towerModel.appliedUpgrades.Contains(UpgradeType.DragonsBreath))
        {
            lightning.Rate /= 3;
            if (towerModel.appliedUpgrades.Contains(UpgradeType.SummonPhoenix))
            {
                damage.damage *= 2;

                if (towerModel.appliedUpgrades.Contains(UpgradeType.WizardLordPhoenix))
                {
                    damage.damage *= 3;
                }

                foreach (var phoenix in towerModel.FindDescendants<TowerModel>("Phoenix"))
                {
                    var phoenixAttack = phoenix.GetAttackModel();
                    var phoenixWeapon = phoenixAttack.weapons.First();

                    var newLightning = lightning.Duplicate();
                    newLightning.animation = 1;

                    newLightning.SetEject(phoenixWeapon.GetEject());

                    newLightning.Rate *= phoenixWeapon.Rate / .2f;

                    phoenixAttack.AddWeapon(newLightning);
                }
            }
        }

        towerModel.GetAttackModel().AddWeapon(lightning);

        if (IsHighestUpgrade(towerModel))
        {
            towerModel.display = towerModel.GetBehavior<DisplayModel>().display =
                Game.instance.model.GetTower(TowerType.WizardMonkey, 0, 0, 1).display;
        }
    }
}