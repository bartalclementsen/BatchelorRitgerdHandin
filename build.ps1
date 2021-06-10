Write-Host("Building Totalview")

dotnet clean ./src/Totalview.sln
dotnet restore ./src/Totalview.sln #-r win-x64
dotnet build ./src/Totalview.Server/ --no-restore #-r win-x64
dotnet build ./src/Totalview.BlazorClient/Server/ --no-restore #-r win-x64
dotnet build ./src/Totalview.TokenUtility/ --no-restore #-r win-x64
dotnet build ./src/Totalview.Testers.ServerStressTester/ --no-restore #-r win-x64

Remove-Item published/PrototypeClient/ -Recurse -ErrorAction Ignore
dotnet publish ./src/Totalview.BlazorClient/Server/ --no-build --no-restore --output published/PrototypeClient/ #-p:PublishSingleFile=true --self-contained true -r win-x64

Remove-Item published/ProxyServer/ -Recurse -ErrorAction Ignore
dotnet publish ./src/Totalview.Server/ --no-build --no-restore --output published/ProxyServer/ #-p:PublishSingleFile=true --self-contained false -r win-x64

Remove-Item published/TokenUtility/ -Recurse -ErrorAction Ignore
dotnet publish ./src/Totalview.TokenUtility/ --no-build --no-restore --output published/TokenUtility/ #-p:PublishSingleFile=true --self-contained false -r win-x64

Remove-Item published/ServerStressTester/ -Recurse -ErrorAction Ignore
dotnet publish ./src/Totalview.Testers.ServerStressTester/ --no-build --no-restore --output published/ServerStressTester/ #-p:PublishSingleFile=true --self-contained false -r win-x64