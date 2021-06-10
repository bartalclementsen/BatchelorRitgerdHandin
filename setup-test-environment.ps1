#param([Parameter(Mandatory=$true)][String]$sqlServer = ".", [Parameter(Mandatory=$true)][String]$sqlUser = "sa", [Parameter(Mandatory=$true)][String]$sqlPassword = "Password0")

Set-Location -Path $PSScriptRoot

If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::"Administrator"))
{
  Start-Process powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($myinvocation.MyCommand.Definition))
  exit
}

Write-Host("Setup Totalview Test Environment")

$sqlServer = Read-Host -Prompt 'Sql Server Name'
$sqlUser = Read-Host -Prompt 'Sql User'
$sqlPassword2 = Read-Host -Prompt 'Sql User Password' -AsSecureString

Write-Host("");

$sqlPassword = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($sqlPassword2))

$sqlDatabase = "Totalview";

$ErrorActionPreference = "Stop"

# functions 
function Execute-Non-Query-Script {
    param( $sqlConnection, $script)

    $commandStrings = $script -split "^\s*GO\s*$", -1, "Multiline"

    $sqlCommand = New-Object System.Data.SqlClient.SqlCommand
    $sqlCommand.Connection = $sqlConnection

    foreach ($line in $commandStrings) 
    {
        if($line) 
        {
            $sqlCommand.CommandText = $line
            $sqlCommand.ExecuteNonQuery() | Out-Null;    
        }
    }
}

$base = Get-Location

# Connect to Database Server
Write-Host("Connectiong to database")

$sqlConnection = New-Object System.Data.SqlClient.SqlConnection
$sqlConnection.ConnectionString = "Data Source=$sqlServer;User ID=$sqlUser;Password=$sqlPassword"
$sqlConnection.Open()

if ($sqlConnection.State -ne [Data.ConnectionState]::Open) {
    "Connection to DB is not open."
    Read-Host -Prompt "Press Enter to exit"
    Exit
}

# Drop database if it exists
Write-Host("Delete database if it exists...")

$sqlCommand = New-Object System.Data.SqlClient.SqlCommand
$sqlCommand.Connection = $sqlConnection
$sqlCommand.CommandText = "IF EXISTS (SELECT name FROM sys.databases WHERE name = N'Totalview') BEGIN alter database Totalview set single_user with rollback immediate; drop database Totalview END"
$sqlCommand.ExecuteNonQuery() | Out-Null;

# Create Database
Write-Host("Creating Database...")

$createDatabaseTV3Sql = [System.IO.File]::ReadAllText("$base\resources\CreateDatabaseTV3.sql")
$createDatabaseTV3Sql = $createDatabaseTV3Sql -replace "NewDBName", $sqlDatabase

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $createDatabaseTV3Sql;

# Update Database
Write-Host("Updating Database...")

$updateDatabaseTV3Sql = [System.IO.File]::ReadAllText("$base\resources\UpdateDatabaseTV3.sql")
$updateDatabaseTV3Sql = $updateDatabaseTV3Sql -replace "NewDBName", $sqlDatabase

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $updateDatabaseTV3Sql;

# Create Basic Data
Write-Host("Creating Basic Data...")

$createBasicDataSql = [System.IO.File]::ReadAllText("$base\resources\CreateBasicData.SQL")
$createBasicDataSql = "USE [$sqlDatabase]; $createBasicDataSql"

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $createBasicDataSql;

# Insert Demo Data
Write-Host("Inserting demo data...")

$demodataSql = [System.IO.File]::ReadAllText("$base\resources\demodata.SQL")
$demodataSql = "USE [$sqlDatabase]; $demodataSql"

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $demodataSql;

# Create configuration tables
Write-Host("Creating configuration tables...")

$createConfigurationTablesSql = [System.IO.File]::ReadAllText("$base\resources\CreateConfigurationTables.sql")
$createConfigurationTablesSql = "USE [$sqlDatabase]; $createConfigurationTablesSql"

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $createConfigurationTablesSql;

# Create Persisted Grant Tables
Write-Host("Creating persisted grant tables...")

$createPersistedGrantTablesSql = [System.IO.File]::ReadAllText("$base\resources\CreatePersistedGrantTables.sql")
$createPersistedGrantTablesSql = "USE [$sqlDatabase]; $createPersistedGrantTablesSql"

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $createPersistedGrantTablesSql;

# Add Clients And Resources
Write-Host("Adding clients and resources...")

$addClientsAndResourcesSql = [System.IO.File]::ReadAllText("$base\resources\AddClientsAndResources.sql")
$addClientsAndResourcesSql = "USE [$sqlDatabase]; $addClientsAndResourcesSql"

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $addClientsAndResourcesSql;

# Add Prototype Client
Write-Host("Adding prototype client...")

$addPrototypeClientSql = [System.IO.File]::ReadAllText("$base\resources\AddPrototypeClient.sql")
$addPrototypeClientSql = "USE [$sqlDatabase]; $addPrototypeClientSql"

Execute-Non-Query-Script -sqlConnection $sqlConnection `
                         -script $addPrototypeClientSql;

#Insert license file
Write-Host("Inserting Demo License file...")

$lic = [System.IO.File]::ReadAllBytes("$base\resources\demo_license.lic")
$sqlCommand = New-Object System.Data.SqlClient.SqlCommand
$sqlCommand.Connection = $sqlConnection
$sqlCommand.CommandText = "USE $sqlDatabase; DELETE FROM LICENSE; INSERT INTO LICENSE (RECID, LICENSE) VALUES (1, @License);"
$sqlCommand.Parameters.Add((New-Object Data.SqlClient.SqlParameter("@License",[Data.SQLDBType]::Image))) | Out-Null
$sqlCommand.Parameters[0].Value = $lic
$sqlCommand.ExecuteNonQuery() | Out-Null;

# Close the connection.
Write-Host("Closing database connection");

if ($sqlConnection.State -eq [Data.ConnectionState]::Open) {
    $sqlConnection.Close()
}

# Remove folder if it exists
if ((Test-Path -Path "environment") -eq $True) {
    Write-Host("Removing environment folder");
    Remove-Item "environment" -Recurse -Force
}

# Extract files
Write-Host("Creating Environment folder and extracting files");

New-Item -Name "environment" -ItemType "directory" | Out-Null
Expand-Archive -Path "resources\environment.zip" -Destination "environment" | Out-Null

# Setup Server Config
Write-Host("Configure server settings");

$serverConfig = "$base\environment\Server\ServerConfig.xml";
$serverConfigXml = [xml](Get-Content $serverConfig)
$serverConfigXml.SelectNodes("//dbTotalView")[0].connectionstring = "Provider=SQLOLEDB.1;Password=$($sqlPassword);User ID=$($sqlUser);Application Name=Totalview3;Initial Catalog=$($sqlDatabase);Data Source=$($sqlServer)"
$serverConfigXml.SelectNodes("//authenticationPortal")[0].enabled = "false"
$serverConfigXml.SelectNodes("//OAuth2")[0].url = "https://localhost:5001"
$serverConfigXml.SelectNodes("//OAuth2")[0].enabled = "false" #Need to get certificate to work before this is ok
$serverConfigXml.Save($serverConfig)


# Add the certificate for Totalview Authentication IS 
Get-ChildItem Cert:\LocalMachine\My | Where-Object { $_.Subject -match 'CN=TotalviewIdentitySigning' } | Remove-Item

$result = New-SelfSignedCertificate `
    -Subject "CN=TotalviewIdentitySigning" `
    -FriendlyName "TotalviewIdentitySigning" `
    -KeyExportPolicy Exportable `
    -TextExtension @('2.5.29.37={text}1.3.6.1.5.5.7.3.3') `
    -KeySpec Signature `
    -HashAlgorithm SHA256 `
    -KeyLength 2048 `
    -CertStoreLocation Cert:\LocalMachine\My `
    -NotAfter (Get-Date).AddYears(10)

$thumbprint = $result.Thumbprint;

$srcStore = New-Object System.Security.Cryptography.X509Certificates.X509Store "MY", "LocalMachine"
$srcStore.Open([System.Security.Cryptography.X509Certificates.OpenFlags]::ReadOnly)
$cert = $srcStore.certificates -match "TotalviewIdentitySigning"
$dstStore = New-Object System.Security.Cryptography.X509Certificates.X509Store "root", "LocalMachine"
$dstStore.Open([System.Security.Cryptography.X509Certificates.OpenFlags]::ReadWrite)
$dstStore.Add($cert[0])

$srcStore.Close | Out-Null
$dstStore.Close | Out-Null


# Setup Totalview Authentication IS
Write-Host("Configure authentication settings");

$TotalviewAuthPortalAppConfig =  "$base\environment\TotalviewAuthenticationIS\appsettings.json"
$TotalviewAuthPortalAppConfigJson = Get-Content $TotalviewAuthPortalAppConfig -raw | ConvertFrom-Json
$TotalviewAuthPortalAppConfigJson.ConnectionStrings.DefaultConnection = "Data Source=$($sqlServer);Initial Catalog=$($sqlDatabase);User ID=$($sqlUser);Password=$($sqlPassword);MultipleActiveResultSets=true"
$TotalviewAuthPortalAppConfigJson.ConnectionStrings.IdentityServerConnection = "Data Source=$($sqlServer);Initial Catalog=$($sqlDatabase);User ID=$($sqlUser);Password=$($sqlPassword);MultipleActiveResultSets=true"
$TotalviewAuthPortalAppConfigJson.IdentityServer.SigningCertificateThumbPrint = $thumbprint;

$TotalviewAuthPortalAppConfigJson | ConvertTo-Json | set-content $TotalviewAuthPortalAppConfig

# Done
Write-Host("");
Write-Host("----------------- DONE ---------------------");
Write-Host("Test Environment is now ready to run");
Write-Host("Run the .\start-test-environment.ps1 script now if you want to startup the environment");
Read-Host -Prompt "Press Enter to exit"