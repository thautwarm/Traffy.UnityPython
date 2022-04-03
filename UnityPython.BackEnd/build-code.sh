rm -rf generated-src/Initialization.cs
rm -rf generated-src/Traffy.Builtins/Bindings.cs
rm -rf generated-src/Traffy.MethodBindings/
# rm -rf generated-src/Traffy.Interfaces/
dotnet publish -c Release UnityPython.csproj --framework netstandard2.1
dotnet run --project ../UnityPython.BackEnd.CodeGen/UnityPython.BackEnd.CodeGen.csproj