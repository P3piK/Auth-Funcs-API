# Auth-Funcs-API
Auth-Funcs-API is a repository for API service written in ASP.NET Core (.NET 6). 
For Notifications Auth-Funcs-API runs a _BackgroundService_ that inserts messages to `Azure Service Bus` which are later received by Auth-Funcs-MQ service. 
Data is stored on `Azure SQL Server`. 
All sensitive configuration is kept using `Azure Key Vault`.

# Key components
- Azure Key Vault
- Azure SQL Server/DB
- Azure Service Bus

# Demo URL
AuthFuncs API is available on https://authfuncsapi.azurewebsites.net/swagger/index.html

#
