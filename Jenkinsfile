pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Publish') {
            steps {
                sh 'dotnet publish --configuration Release --output ./publish'
            }
        }
    }
    post {
        always {
            // Archive the published files as build artifacts
            archiveArtifacts artifacts: 'publish/**', allowEmptyArchive: true
            // Clean up the workspace after the build
            cleanWs()
        }
    }
}
