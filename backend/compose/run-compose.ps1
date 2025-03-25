# Using example
# .\run-compose.ps1 -env dev
 

param(
    [string]$env
)

if (-not $env) {
    Write-Host "Please specify an environment!"
    Write-Host "Example: .\run-compose.ps1 -env dev"
    exit 1
}

# Select the corresponding docker-compose file
switch ($env) {
    "pg" {
        $composeFile = "docker-compose.dev.pg.yml"
    }
    "api-pg" {
        $composeFile = "docker-compose.dev.api-pg.yml"
    }
    "dev" {
        $composeFile = "docker-compose.dev.yml"
    }
    default {
        Write-Host "Unknown environment: $env"
        Write-Host "Valid options: api-pg, dev"
        exit 1
    }
}

# Run docker-compose
Write-Host "Starting with $composeFile ..."
docker-compose -f $composeFile up --build