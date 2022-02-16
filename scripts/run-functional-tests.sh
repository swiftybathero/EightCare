#!/bin/bash
cd tests/EightCare.API.FunctionalTests
dotnet build
dotnet test --no-build --logger:"console;verbosity=normal"
