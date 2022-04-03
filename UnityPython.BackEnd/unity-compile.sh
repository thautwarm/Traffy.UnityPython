dotnet publish -c Release UnityPython.csproj
GAME_DIR="C:/Users/twshe/Storage/Game/StoryTeller/StoryTeller/Assets/Scripts"
cp -r bin/Release/netstandard2.0/*.dll $GAME_DIR
