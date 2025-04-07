#!/bin/bash
set -e

echo "📝 Committing updated submodule references..."

git add .
git commit -m "Update submodule references"
