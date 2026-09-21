#!/usr/bin/env bash
set -euo pipefail

root_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
game_path="${GAME_PATH:-$HOME/Library/Application Support/Steam/steamapps/common/Stardew Valley/Contents/MacOS}"
mods_dir="${GAME_MODS_DIR:-$game_path/Mods}"
output_dir="$root_dir/bin/Debug/net6.0"
mod_link="$mods_dir/TitleScreenScale"

if command -v dotnet >/dev/null 2>&1; then
    dotnet_command=(dotnet)
elif command -v mise >/dev/null 2>&1; then
    dotnet_command=(mise exec dotnet@6 -- dotnet)
else
    echo "Install .NET 6 or mise before building." >&2
    exit 1
fi

"${dotnet_command[@]}" build "$root_dir/TitleScreenScale.csproj" \
    --configuration Debug \
    -p:GamePath="$game_path" \
    -p:EnableModDeploy=false \
    -p:EnableModZip=false

python3 - "$root_dir/TitleScreenScale.csproj" "$output_dir/manifest.json" <<'PY'
import re
import sys
from pathlib import Path

project_path, manifest_path = map(Path, sys.argv[1:])
project = project_path.read_text()
version = re.search(r"<Version>([^<]+)</Version>", project).group(1)
manifest = (project_path.parent / "manifest.json").read_text().replace("%ProjectVersion%", version)
manifest_path.write_text(manifest)
PY

mkdir -p "$mods_dir"
if [[ -e "$mod_link" && ! -L "$mod_link" ]]; then
    echo "$mod_link already exists and is not a symlink; move it before linking the local build." >&2
    exit 1
fi

ln -sfn "$output_dir" "$mod_link"
echo "Linked $mod_link -> $output_dir"
