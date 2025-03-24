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
  api-pg)
    COMPOSE_FILE="docker-compose.dev.api-pg.yml"
    ;;
  dev)
    COMPOSE_FILE="docker-compose.dev.yml"
    ;;
  *)
    echo "❌ Unknown environment: $1"
    echo "Valid options: api-pg, dev"
    exit 1
    ;;
esac

# Run docker-compose
echo "🚀 Starting with $COMPOSE_FILE ..."
docker-compose -f $COMPOSE_FILE up --build
