rm -rf generated-src/BuiltinBindings.cs
rm -rf generated-src/MethodBindings/
dotnet publish -c Release UnityPython.csproj
dotnet run --project ../UnityPython.BackEnd.CodeGen/UnityPython.BackEnd.CodeGen.csproj