#!/bin/bash
cd tests/EightCare.API.IntegrationTests
dotnet build
dotnet test --no-build --logger:"console;verbosity=normal"
