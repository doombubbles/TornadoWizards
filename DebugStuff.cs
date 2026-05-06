using BTD_Mod_Helper.Api.Enums;
using Il2CppAssets.Scripts.Models.Towers;
using PathsPlusPlus;

#if DEBUG
namespace TornadoWizards;

public class Sharknado : UpgradePlusPlus<TornadoWizardPath>
{
    public override int Cost => 100000;
    public override int Tier => 6;
    public override string Icon => VanillaSprites.MegalodonUpgradeIcon;
    public override string Portrait => "Wizard5";

    public override string Description => "Sharknado lol.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {

    }
}

internal class KingOfDarknessPath : PathPlusPlus
{
    public override string Tower => TowerType.WizardMonkey;
    public override int ExtendVanillaPath => Bottom;
}

internal class QueenOfDarknessPath : PathPlusPlus
{
    public override string Tower => TowerType.WizardMonkey;
    public override int ExtendVanillaPath => Bottom;
}

internal class KingOfDarkness : UpgradePlusPlus<KingOfDarknessPath>
{
    public override int Cost => 100000;
    public override int Tier => 6;

    public override string Description => "kingy";

    public override string Icon => VanillaSprites.SoulbindUpgradeIcon;
}

internal class QueenOfDarkness : UpgradePlusPlus<QueenOfDarknessPath>
{
    public override int Cost => 70000;
    public override int Tier => 6;

    public override string Description => "queeny";

    public override string Icon => VanillaSprites.SoulbindUpgradeIcon;
}

#endif