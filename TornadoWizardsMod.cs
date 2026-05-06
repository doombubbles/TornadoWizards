using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppNinjaKiwi.Common;
using Il2CppNinjaKiwi.Localization;
using PathsPlusPlus;
using TornadoWizards;

[assembly: MelonInfo(typeof(TornadoWizardsMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TornadoWizards;

public class TornadoWizardsMod : BloonsTD6Mod
{
    public static readonly ModSettingBool BallLightning = new(false)
    {
        description = """
                      Uses Ball Lightning as the Tier 4 Upgrade instead of the custom Lightning Overload upgrade. 
                      Also reincorporates the Ball Lightning projectiles into the Tier 5 Upgrade.
                      """,
        icon = VanillaSprites.BallLightningUpgradeIcon,
        onSave = _ => ModContent.GetInstance<ElectricOverload>().RegisterText(LocalizationManager.Instance.defaultTable)
    };
}

public class TornadoWizardPath : PathPlusPlus
{
    public override string Tower => TowerType.WizardMonkey;
}