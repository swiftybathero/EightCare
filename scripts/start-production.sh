#!/bin/bash
export ASPNETCORE_ENVIRONMENT=Production
cd src/EightCare.API
dotnet run --no-launch-profile
