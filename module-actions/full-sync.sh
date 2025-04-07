#!/bin/bash
set -e

echo "🔁 Full sync: pull + update submodules"

git pull
./module-actions/update-submodules.sh
./module-actions/commit-submodule-updates.sh