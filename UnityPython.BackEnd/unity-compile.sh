dotnet publish -c Release UnityPython.csproj --framework netstandard2.0
GAME_DIR="C:/Users/twshe/Storage/Game/StoryTeller/StoryTeller/Assets/Scripts"
cp -r bin/Release/netstandard2.0/UnityPython.dll $GAME_DIR
