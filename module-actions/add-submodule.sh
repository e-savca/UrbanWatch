#!/bin/bash
set -e

if [ -z "$1" ] || [ -z "$2" ]; then
  echo "Usage: ./add-submodule.sh <repo-url> <path>"
  exit 1
fi

REPO_URL=$1
TARGET_PATH=$2

echo "➕ Adding submodule: $REPO_URL at $TARGET_PATH..."

git submodule add "$REPO_URL" "$TARGET_PATH"
git submodule update --init --recursive
git add .gitmodules "$TARGET_PATH"
git commit -m "Add submodule: $TARGET_PATH"
