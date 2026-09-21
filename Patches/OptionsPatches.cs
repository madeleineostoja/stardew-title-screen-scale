using HarmonyLib;
using StardewValley;

namespace TitleScreenScale.Patches;

[HarmonyPatch(typeof(Options), nameof(Options.desiredUIScale), MethodType.Getter)]
internal static class OptionsPatches
{
    [HarmonyPostfix]
    private static void DesiredUIScalePostfix(ref float __result)
    {
        if (Game1.gameMode == 3)
            return;

        var viewport = Game1.graphics.GraphicsDevice.Viewport;
        var widthScale = viewport.Width / (float)Game1.defaultResolutionX;
        var heightScale = viewport.Height / (float)Game1.defaultResolutionY;
        __result = Math.Min(1f, Math.Min(widthScale, heightScale));
    }
}
