#!/bin/bash
cd tests/EightCare.API.FunctionalTests

config=${1:-Debug}

echo Detected configuration: $config

dotnet test -c $config --logger:"console;verbosity=normal"
