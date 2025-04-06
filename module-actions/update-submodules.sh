#!/bin/bash

echo "🔄 Updating submodules to latest commits from origin..."

git submodule init
git submodule update

git submodule foreach '
  echo "📁 Updating $name ..."
  git checkout main || echo "⚠️ Could not checkout main in $name"
  git pull origin main || echo "⚠️ Could not pull from origin/main in $name"
'

echo "✅ Submodules updated to latest commits."
