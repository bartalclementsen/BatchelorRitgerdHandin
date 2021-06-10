Write-Host("Starting Totalview Blazor Client...")
Push-Location ./published/PrototypeClient/
Start-Process -FilePath "powershell" -ArgumentList "-noexit", "-NoLogo", "-Command dotnet Totalview.BlazorClient.Server.dll --environment Development --urls 'https://localhost:5005;http://localhost:5004'"
Pop-Location

Write-Host("Waiting for Totalview Blazor Client...")
Start-Sleep -Seconds 2
Start-Process "https://localhost:5005"