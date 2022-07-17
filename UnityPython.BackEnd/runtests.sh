# check if the input argument is set to be 'debug'
if [ "$1" == "debug" ]; then
  dir="tests_debug"
else
  dir="tests"
fi

rm -rf out
upycc $dir --includesrc --recursive --outdir out
dotnet run --project RunUnityPython.Test.csproj --framework net6 -c Release out
