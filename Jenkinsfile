pipeline{
  agent any
  stages{
    stage("Clean Up"){
      steps{
        deleteDir()
      }
    }

    stage("Build"){
      steps{
        // dir("addy-mail-builder"){
          sh "dotnet clean"
          sh "dotnet restore"
          sh "dotnet build"
        // }
      }
    }

    stage("Test"){
      steps{
        dir("addy-mail-builder"){
          sh "dotnet test"
        }
      }
    }
  }
}