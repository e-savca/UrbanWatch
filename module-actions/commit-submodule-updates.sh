#!/bin/bash

echo "💾 Committing submodule references in main repo..."

git add .
git commit -m "🔁 Update submodules to latest commits"
git push

echo "✅ Submodule changes pushed to main repo."
