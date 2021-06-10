# Totalview

This is the implementation of the BA theses **Implementing a new communication framework for a legacy system**.

## Requirements

- .NET 5, available at https://dotnet.microsoft.com/download
- (Optional) Docker, available at https://www.docker.com/products/docker-desktop
- MSSQL database server, available at https://www.microsoft.com/en-us/sql-server/sql-server-downloads or as a Docker Image at https://hub.docker.com/_/microsoft-mssql-server

##  How to start the test environment

The test environmment requires that you have a Totalview Server running and a Totalview Database in a MSSQL database server.

**To setup** start phowershell *as an administrator*, navigate to this folder and run this script:

````cmd
.\setup-test-environment.ps1
````

**NB!** you need to specify the address to your MSSQL server with a db user that is sysadmin. This is required because it will create a database with test data.

**To start** start phowershell *as an administrator*, navigate to this folder and run this script:

````cmd
.\start-test-environment.ps1
````

## How to build

You can build the releveant projects by running the following code in powershell 

````cmd
.\build.ps1
````

You can also build the solution by opening it in visual studi 2019 or later and building it.


## How to start

You can start the releveant projects by running the following code in powershell 

````cmd
.\start.ps1
````

Open the .src/Totalview.sln in visual studio 2019 or later.

1) Start the Totalview.Server
2) Start the Totalview.BlazorClient.Server
3) You can login with the user: *bac* with password: *1234*
4) If you need a pure admin user it is *admin* with password: *Password0*

## Reading the project

All items related to writing the project are located in the **writing** folder. To view the project open the **writing\Project\Project.docx** file.

## Testing GRPC

To run these test tools Docker is required.

### GRPC UI
````cmd
docker run --rm -p 8081:8080 --name grpcui fullstorydev/grpcui -insecure -vvv host.docker.internal:5003
````

### GRPC URL
````cmd
docker run --rm --name grpcurl fullstorydev/grpcurl -insecure host.docker.internal:5003 describe
docker run --rm --name grpcurl fullstorydev/grpcurl -insecure host.docker.internal:5003 totalview.v1.TotalviewService/Subscribe
docker run --rm --name grpcurl fullstorydev/grpcurl -insecure host.docker.internal:5003 totalview.v1.TotalviewService/GetAllAppointments
docker run --rm --name grpcurl fullstorydev/grpcurl -insecure host.docker.internal:5003 totalview.v1.TotalviewService/SetCurrentState
````

## How to Run the Totalview.Testers.ServerStressTester

1. First you need to disable the authentication on the in **Totalview.Server.GrpcServices.TotalviewServiceImplementation** by removing (or commenting out the [Authorize] attribute)
2. Run the .\Build.ps1 to make a build that does not require authentication
3. Start the Testing Environment (see above)
4. Run the .\start-proxy-server-with-stress-test.ps1 script to start the server and the Stress tester
5. Set the number of clients to the desired amount.
6. If you are using the default launch options, the address does not need to be changed and you can click connect
7. Boot up a classic Totalview Client and change state. This will trigger an envent that will be sent to all the connected clients
8. Verify that all clients have gotten the mesage and that the time isn't to much from the lowest to to hightes
