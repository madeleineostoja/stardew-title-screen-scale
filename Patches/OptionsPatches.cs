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
    private static void DesiredUIScalePostfix(Options __instance, ref float __result)
    {
        if (Game1.gameMode == 3)
            return;

        var viewport = Game1.uiViewport;
        if (viewport.Width <= 0 || viewport.Height <= 0)
            return;

        // Include the current base scale so this composes with platform-level UI scaling.
        var widthScale = __instance.baseUIScale * viewport.Width / MinimumCanvasWidth;
        var heightScale = __instance.baseUIScale * viewport.Height / MinimumCanvasHeight;
        __result = Math.Min(1f, Math.Min(widthScale, heightScale));
    }
}

[HarmonyPatch(typeof(Options), nameof(Options.zoomLevel), MethodType.Getter)]
internal static class OptionsZoomPatches
{
    [HarmonyPostfix]
    private static void ZoomLevelPostfix(Options __instance, ref float __result)
    {
        // Title-screen overlays use world coordinates even though Stardew draws the menu in UI coordinates.
        if (Game1.gameMode != 3)
            __result = __instance.uiScale;
    }
}

// Switching coordinate systems must also rebuild render targets when the saved scales happen to match.
[HarmonyPatch(typeof(Game1), nameof(Game1.gameMode), MethodType.Setter)]
internal static class GameModePropertyPatches
{
    [HarmonyPrefix]
    private static void GameModePrefix(byte value, out bool __state)
    {
        __state = value != 11 && Game1.gameMode != 11 && (Game1.gameMode == 3) != (value == 3);
    }

    [HarmonyPostfix]
    private static void GameModePostfix(bool __state)
    {
        if (__state)
            Game1.game1.refreshWindowSettings();
    }
}

[HarmonyPatch(typeof(Game1), nameof(Game1.setGameMode))]
internal static class SetGameModePatches
{
    [HarmonyPrefix]
    private static void SetGameModePrefix(byte mode, out bool __state)
    {
        __state = mode != 11 && Game1.gameMode != 11 && (Game1.gameMode == 3) != (mode == 3);
    }

    [HarmonyPostfix]
    private static void SetGameModePostfix(bool __state)
    {
        if (__state)
            Game1.game1.refreshWindowSettings();
    }
}
