pipeline {
    agent {
        node {
            label 'jenkins-agent-dotnet'
        }
    }
    triggers {
        pollSCM '1 * * * *'
    }
    stages {
        stage('Restore') {
            steps {
                echo 'before dotnet restore'
                sh 'dotnet restore'
                echo 'after dotnet restore'
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