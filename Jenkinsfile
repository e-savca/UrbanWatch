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
                ls -l
                cd backend/dotnet-api/vssln/UrbanWatchAPI
                ls -l
                dotnet restore
                '''
            }
        }
        stage('Build') {
            steps {
                echo 'before dotnet build'
                sh 'dotnet build --configuration Release'
                echo 'after dotnet build'
            }
        }
        stage('Publish') {
            steps {
                echo 'before dotnet publish'
                sh 'dotnet publish --configuration Release --output ./publish'
                echo 'after dotnet publish'
            }
        }
    }
}