# Title Screen Scale

Title Screen Scale automatically shrinks Stardew Valley's pre-game interface when needed to keep it inside the render viewport. This keeps the title screen and its submenus visible without changing the player's gameplay UI scale.

## Requirements

- Stardew Valley 1.6
- SMAPI 4.0 or later

## Install and remove

1. Install SMAPI.
2. Unzip the release into Stardew Valley's `Mods` directory.
3. Start the game.

The mod has no configuration. To remove it, delete its folder from `Mods`; it does not write save data.

## Behaviour

Before gameplay, the mod compares Stardew Valley's logical UI viewport with a 1300×744 safe-area canvas and uses the smaller width or height ratio, capped at 100%. It applies the same effective scale to the pre-game scene and UI so controller cursors and title-screen controls added by other mods remain in the same coordinate space. The safe area includes controls that the Load menu places beyond Stardew Valley's nominal 1280×720 UI canvas. This covers the title screen, Load, New Game, Co-op, character creation, and related title submenus without detecting specific devices or menus.

Once gameplay starts, Stardew Valley's normal UI scale is left untouched.

## Development

Run `./build-local.sh` to build a debug copy and symlink it into the default macOS Steam installation. Set `GAME_PATH` to use another game directory, or `GAME_MODS_DIR` to override only the destination `Mods` directory.
