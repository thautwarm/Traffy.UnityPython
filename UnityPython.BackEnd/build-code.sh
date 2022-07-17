rm -rf generated-src/Initialization.cs
rm -rf generated-src/Traffy.Builtins/Bindings.cs
rm -rf generated-src/Traffy.MethodBindings/
# rm -rf generated-src/Traffy.Interfaces/
powershell -c 'dotnet publish -c Release RunUnityPython.csproj --framework netstandard2.1 /p:DefineConstants="CODE_GEN%3BFAST_ITER_BLIST%3BNOT_UNITY"'
dotnet run --project ../UnityPython.BackEnd.CodeGen/UnityPython.BackEnd.CodeGen.csproj
powershell -c 'dotnet publish -c Release RunUnityPython.csproj --framework net6 /p:DefineConstants="FAST_ITER_BLIST%3BNOT_UNITY"'