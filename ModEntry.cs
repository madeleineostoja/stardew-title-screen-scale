using HarmonyLib;
using StardewModdingAPI;

namespace TitleScreenScale;

internal sealed class ModEntry : Mod
{
    public override void Entry(IModHelper helper)
    {
        new Harmony(ModManifest.UniqueID).PatchAll();
    }
}
