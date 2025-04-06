#!/bin/bash

## Make this script executable with:
## chmod +x run-compose.sh

# Check if an argument was provided
if [ -z "$1" ]; then
  echo "❌ Please specify an environment!"
  echo "Example: ./run-compose.sh dev-api"
  exit 1
fi

# Select the corresponding docker-compose file
case "$1" in
  api-worker)
    COMPOSE_FILE="docker-compose.dev.api-worker.yml"
    ;;
  api)
    COMPOSE_FILE="docker-compose.dev.api.yml"
    ;;
  dev)
    COMPOSE_FILE="docker-compose.dev.yml"
    ;;
  *)
    echo "Using default 'docker-compose.yml' file"
    COMPOSE_FILE="docker-compose.yml"
    ;;
esac

# Run docker-compose
echo "🚀 Starting with $COMPOSE_FILE ..."
docker-compose -f $COMPOSE_FILE up --build
