# Overview

Online QR Code generator and reader using webcam

Live demo available at https://qrcode.oxage.net

![Screenshot of QR Code generator](https://raw.githubusercontent.com/papnkukn/qrcode-online/master/Wiki/generator.png)

![Screenshot of QR Code scanner](https://raw.githubusercontent.com/papnkukn/qrcode-online/master/Wiki/scanner.png)

## Minimum Requirements

ASP.NET Core 2.1

## Getting Started

On Windows using Visual Studio
1. Clone the repository
```
git clone https://github.com/papnkukn/qrcode-online.git
```
2. Open the solution file `QRCode.sln` in Visual Studio 2017 or later
3. Hit F5 to run

## Setup for production

Install .NET Core 2.1 on Ubuntu 18.04, also you can use other OS, Windows, Mac OS X
```
wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo add-apt-repository universe
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-sdk-2.1
```

Clone the repository
```
git clone https://github.com/papnkukn/qrcode-online.git
```

Navigate to the downloaded folder
```
cd qrcode-online
```

Build a release
```
dotnet publish --configuration Release
```

Go to the output directory
```
cd bin/Release/netcoreapp2.1/publish
```

Start on port 3000
```
dotnet QRCode.dll --port 3000
```

## REST API

Decode QR Code image
```
POST /api/v1/decode
```

Request
```javascript
{
  contentType: "image/jpeg",
  encoding: "base64",
  content: "..."
}
```

Response
```javascript
{
  status: "success",
  type: "qr_code",
  text: "Hello World"
}
```

## Legal notice

```
Licensed under the MIT license:
http://www.opensource.org/licenses/mit-license.php

The word "QR Code" is registered trademark of DENSO WAVE INCORPORATED
http://www.denso-wave.com/qrcode/faqpatent-e.html
```