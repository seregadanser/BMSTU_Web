@echo off
setlocal

set "appSourcePath=D:\Tests\Web\WebApplication1\bin\Debug\net6.0"

cd "%appSourcePath%"
start .\WebApplication1.exe --urls http://127.0.0.1:5001
cd D:/Tests/Web/

npm run start --prefix Front/