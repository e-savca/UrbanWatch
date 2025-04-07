#!/bin/bash
set -e

echo "🔄 Updating all submodules to latest 'origin/main'..."

# Init submodules if not already
git submodule update --init --recursive

# Pull latest for each submodule
git submodule foreach '
  echo "➡️ Updating $name ..."
  git fetch origin
  git checkout main
  git pull origin main
'
