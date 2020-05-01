# Weight Tracker API

I wanted a way to store all my weigh-ins and at the time I was being trained in C# so I took the opportunity to build an API with .NET Core and use SQL Server as the data store.

## Install software (Linux)
### .NET Core
- Add package signing key
```
wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
```
- Install
```
sudo apt-get update
sudo apt-get install dotnet-sdk-3.1
```
### SQL Server 
- Import key
```
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
```
- Update and install
```
sudo apt-get update
sudo apt-get install -y mssql-server
```
- Run setup
```
sudo /opt/mssql/bin/mssql-conf setup
```
## Setup 
- Copy the appsettings-example.json to a new file named appsettings.Development.json
```
cp appsettings-example.json appsettings.Development.json
```
- Fill in parameters of config file
  - OriginHosts - Add urls for sites that are accessible to API
  - ConnectionStrings - SqlServer - Add connection details to SQL Server database
  - JWT - Key - Add a secret key to be used for authentication
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "OriginHosts": [
    "http://localhost:4200"
  ],
  "ConnectionStrings": {
    "SqlServer": "Data Source=;Database =;User ID=;Password=",
    "AddTestData": true
  },
  "Jwt": {
    "Key": "",
    "Issuer": "Weight Tracker API",
    "Audience": "Weight Tracker API Users",
    "AccessExpiration": 3600,
    "RefreshExpiration": 60
  },
  "HttpsRedirect": false
}
```
- Install EF tool
```
dotnet tool install --global dotnet-ef
```
- Run migrations
```
dotnet ef database update
```
- Install dependancies
```
dotnet restore
```
## Commands
- Build files
```
dotnet build
```
- Run app
```
dotnet run
```
