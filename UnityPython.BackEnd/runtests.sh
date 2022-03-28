# check if the input argument is set to be 'debug'
if [ "$1" == "debug" ]; then
  dir="tests_debug"
else
  dir="tests"
fi

rm -rf out
unitypython.exe $dir --includesrc --recursive --outdir out
dotnet run --project UnityPython.Tests.csproj -c Release out
