pipeline {
    agent {
        node {
            label 'jenkins-agent-dotnet'
        }
    }
    triggers {
        pollSCM '* * * * *'
    }
    stages {
        stage('Restore') {
            steps {
                sh '''
                cd backend/dotnet-api/vssln/UrbanWatchAPI
                dotnet restore
                '''
            }
        }
        stage('Build') {
            steps {
                sh '''
                cd backend/dotnet-api/vssln/UrbanWatchAPI
                dotnet build --configuration Release
                '''
            }
        }
        stage('Publish') {
            steps {
                sh '''
                cd backend/dotnet-api/vssln/UrbanWatchAPI
                dotnet publish --configuration Release --output ./publish
                '''
            }
        }
    }
    post {
        always {
            script {
                def publishDir = 'backend/dotnet-api/vssln/UrbanWatchAPI/publish'
                sh "ls -l ${publishDir}" // Debugging: Check if files exist
            }

            // Archive the correct publish directory
            archiveArtifacts artifacts: 'backend/dotnet-api/vssln/UrbanWatchAPI/publish/**', allowEmptyArchive: true

            // Clean up workspace after archiving
            cleanWs()
        }
    }
}