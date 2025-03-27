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
    "api" {
        $composeFile = "docker-compose.dev.api.yml"
    }
    "dev" {
        $composeFile = "docker-compose.dev.yml"
    }
    default {
        Write-Host "Using default 'docker-compose.yml' file"
        $composeFile = "docker-compose.yml"
    }
}

# Run docker-compose
Write-Host "Starting with $composeFile ..."
docker-compose -f $composeFile up --build