#!/bin/bash
cd tests/FunctionalTests/EightCare.API.FunctionalTests

runAgainstProd=${1:-false}

echo Running against Production database: $runAgainstProd

dotnet test -c Release -e RunAgainstProductionDatabase=$runAgainstProd --logger:"console;verbosity=normal" 
