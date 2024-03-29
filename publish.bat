cd "EV Car UI"
dotnet publish -r win-x64 -p:Configuration=Release -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:UseAppHost=true --self-contained true
dotnet publish -r linux-arm64 -p:Configuration=Release -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:UseAppHost=true --self-contained true
dotnet publish -r linux-arm -p:Configuration=Release -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:UseAppHost=true --self-contained true
dotnet publish -r linux-x64 -p:Configuration=Release -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:UseAppHost=true --self-contained true

cd ..

rm output/*.zip

7z a "output/rasberrypi-64bit.zip" "./EV Car UI/bin/Release/net6.0/linux-arm64/publish/*"
7z a "output/rasberrypi-32bit.zip" "./EV Car UI/bin/Release/net6.0/linux-arm/publish/*"
7z a "output/linux.zip" "./EV Car UI/bin/Release/net6.0/linux-x64/publish/*"
7z a "output/windows.zip" "./EV Car UI/bin/Release/net6.0/win-x64/publish/*"
