
@echo off
setlocal

set "dllSourcePath=WebBack"
set "dllOutputPath=WebBack\WebBack\bin\Debug\net6.0\WebBack.dll"
set "appSourcePath=WebApplication1"

echo "build DLL"

cd "%dllSourcePath%"
dotnet build

cd ..

if not exist "%dllOutputPath%" (
    echo "DLL not found"
    pause
    exit /b 1
)

echo "build App"

cd "%appSourcePath%"
dotnet build 

cd ..
npm run build --prefix Front/


