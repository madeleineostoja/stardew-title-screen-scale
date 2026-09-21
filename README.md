# Title Screen Scale

Title Screen Scale automatically shrinks Stardew Valley's pre-game interface when the game is rendering below its native 1280×720 UI canvas. This keeps the title screen and its submenus inside smaller render viewports without changing the player's gameplay UI scale.

## Requirements

- Stardew Valley 1.6
- SMAPI 4.0 or later

## Install and remove

1. Install SMAPI.
2. Unzip the release into Stardew Valley's `Mods` directory.
3. Start the game.

The mod has no configuration. To remove it, delete its folder from `Mods`; it does not write save data.

## Behaviour

Before gameplay, the mod compares the current render viewport with Stardew Valley's 1280×720 logical baseline and uses the smaller width or height ratio, capped at 100%. This covers the title screen, Load, New Game, Co-op, character creation, and related title submenus without detecting specific devices or menus.

Once gameplay starts, Stardew Valley's normal UI scale is left untouched.

## Development

Run `./build-local.sh` to build a debug copy and symlink it into the default macOS Steam installation. Set `GAME_PATH` to use another game directory, or `GAME_MODS_DIR` to override only the destination `Mods` directory.
