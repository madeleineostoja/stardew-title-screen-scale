using HarmonyLib;
using StardewValley;

namespace TitleScreenScale.Patches;

[HarmonyPatch(typeof(Options), nameof(Options.desiredUIScale), MethodType.Getter)]
internal static class OptionsPatches
{
    // The Load menu extends beyond Stardew's nominal 1280×720 canvas.
    private const float MinimumCanvasWidth = 1300f;
    private const float MinimumCanvasHeight = 744f;

    [HarmonyPostfix]
    private static void DesiredUIScalePostfix(ref float __result)
    {
        if (Game1.gameMode == 3)
            return;

        var viewport = Game1.graphics.GraphicsDevice.Viewport;
        var widthScale = viewport.Width / MinimumCanvasWidth;
        var heightScale = viewport.Height / MinimumCanvasHeight;
        __result = Math.Min(1f, Math.Min(widthScale, heightScale));
    }
}
