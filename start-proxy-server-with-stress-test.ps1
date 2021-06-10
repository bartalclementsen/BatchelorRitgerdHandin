Write-Host("Starting Totalview Server...")
Push-Location ./published/ProxyServer/
Start-Process -FilePath "powershell" -ArgumentList "-noexit", "-NoLogo", "-Command dotnet Totalview.Server.dll --environment Development --urls 'https://localhost:5003;http://localhost:5002'"
Pop-Location

Write-Host("Starting Server Stress Tester...")
Push-Location ./published/ServerStressTester/
Start-Process -FilePath "Totalview.Testers.ServerStressTester.exe"
Pop-Location