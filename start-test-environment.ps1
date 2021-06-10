Set-Location -Path $PSScriptRoot

If (-NOT ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::"Administrator"))
{
  Start-Process powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($myinvocation.MyCommand.Definition))
  exit
}

Write-Host("Starting Totalview Authentication")
Push-Location environment\TotalviewAuthenticationIS
Start-Process .\Totalview.Authentication.IdentityServer.exe
Pop-Location

Write-Host("Starting Totalview Server")
Push-Location environment\Server
Start-Process .\Totalview3Server.exe /file:serverconfig.xml
Pop-Location

Write-Host("-------------- STARTED ---------------")