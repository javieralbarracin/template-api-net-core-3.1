pipeline {
	   agent any
		
	   environment {
				dotnet = "C:\\Program Files\\dotnet\\dotnet.exe"			        			
				pathSolucion = "D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.sln"	
				pathProject = "D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.Api\\MisGastos.Api.csproj"	
				//projectSonarName = "/k:MisGastosApi"
				urlSonar = "/d:sonar.host.url=http://localhost:9123"
				projectSonarKey = "/d:sonar.login=ad18104b1c66059dc368e915bee16f69b164173c"
				pathPubli = "D:\\DEVELOP\\PROYECTOS.LABS\\Publicaciones\\MisGastosWebApi"
				pathProjectPublish = "D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.Api\\bin\\Debug\\netcoreapp3.1\\publish"
				ENV_NAME = "${env.BRANCH_NAME}"			
				//scannerHome = tool 'SonarQube Scanner - MSBuild'
	   }
       stages {
			stage('Clean'){
					steps{
						bat " \"${dotnet}\" clean \"${pathProject}\" "
					}
			}
			stage('Build') { 
				steps{
				  bat ''' dotnet build D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.Api\\MisGastos.Api.csproj /p:Configuration=Release --configuration test '''
				}
			}			
			stage('Publish Desa'){				
				steps{
				  bat ''' dotnet publish D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.Api\\MisGastos.Api.csproj /p:EnvironmentName=test '''
				  //bat ''' appcmd stop apppool /apppool.name:DefaultAppPool '''
				  bat ''' Xcopy /S /E /C /H /O /R /Y /D /V D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.Api\\bin\\Debug\\netcoreapp3.1\\publish D:\\DEVELOP\\PROYECTOS.LABS\\Publicaciones\\MisGastosWebApi '''
				  //bat ''' Xcopy /S /E /C /H /O /R /Y /D /V D:\\Jenkins\\workspace\\MisGastosApi\\MisGastos.Api\\bin\\D\\netcoreapp3.1\\publish C:\\inetpub\\wwwroot\\MisGastosWebApi '''				  				  				  				  
				}
			}			
		}
		post{
				always {
					echo "post always"
				}               
				success { 
					  mail to: 'javieralbarracin881@yahoo.com.ar',
					  subject: "OK CI: Project name -> ${env.JOB_NAME}",					  
					  body: "<b>CI/CD - Team Devops</b><br>Project: ${env.JOB_NAME} <br>Build Number: ${env.BUILD_NUMBER} <br> URL de build: ${env.BUILD_URL}", 
					  cc: '', 
					  charset: 'UTF-8', 
					  from: '', 
					  mimeType: 'text/html', 
					  replyTo: ''					  
				}  
				failure {  
					  mail to: 'javieralbarracin881@yahoo.com.ar',
					  subject: "Error CI: Project name -> ${env.JOB_NAME}",					  
					  body: "<b>CI/CD - Team Devops</b><br>Project: ${env.JOB_NAME} <br>Build Number: ${env.BUILD_NUMBER} <br> URL de build: ${env.BUILD_URL}", 
					  cc: '', 
					  charset: 'UTF-8', 
					  from: '', 
					  mimeType: 'text/html', 
					  replyTo: ''				
				}
				unstable {  
					echo 'This will run only if the run was marked as unstable'  
				}  
				changed {  
					echo 'This will run only if the state of the Pipeline has changed'  
					echo 'For example, if the Pipeline was previously failing but is now successful'  
				} 				
				  
            }
	}