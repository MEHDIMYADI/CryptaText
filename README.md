# CryptaText
CryptaText is a simple, cross-platform text encryption and decryption app built with .NET MAUI.  It supports multiple languages (English & Persian), theme switching (Light, Dark, System),  and convenient features like copy/paste for input and output fields.  Designed for quick and secure text processing on Windows, Android, macOS, and Linux.

# Screenshots
| ![Screen1](Screenshots/Screenshot%201.png) | ![Screen2](Screenshots/Screenshot%202.png) | ![Screen3](Screenshots/Screenshot%203.png) | ![Screen4](Screenshots/Screenshot%204.png) |
|---|---|---|---|

# Build
Make sure you have .NET 10 SDK and MAUI workload installed.

## Windows
```bash
dotnet restore
dotnet publish -f net10.0-windows10.0.19041.0 -c Release
```
### The output will be available in:
bin/Release/net10.0-windows10.0.19041.0/win10-x64/publish/

## Android
```bash
dotnet publish -f net10.0-android -c Release
```
### The output will be available in:
bin\Release\net10.0-android

## iOS / Mac
```
> iOS and MacCatalyst builds require macOS.
