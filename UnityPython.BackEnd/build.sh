rm -rf generated-src/BuiltinBindings.cs
rm -rf generated-src/MethodBindings/
dotnet publish -c Release && dotnet run --project ../UnityPython.BackEnd.CodeGen/UnityPython.BackEnd.CodeGen.csproj