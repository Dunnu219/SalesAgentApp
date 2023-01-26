# SalesAgentApp
Sales Agent App-  UI, API and SQL

# UI
The UI is developed in Angular 15. Editor used is VS Code
Node.js and Angular CLI need to be installed
Node.js can be installed from https://nodejs.org/en/

Angular CLI can be installed by running the command 
npm install -g @angular/cli 
in command prompt

For the App to run the node modules need to be installed first by running a command 
npm install
in the terminal.

Then command
ng serve 
would run the application on http://localhost:4200/ 

Before running the UI, the API should be running as the UI interacts with the API.

Styling is not much done on the UI so the controls might be misaligned a bit. Concentrated mainly on the functionality.

# API
The API is developed in .Net 6. IDE used is Visual Studio 2022

The API connects to to local MS SQL

The connection string is in the appsettings.json file which should be updated accordingly based on where the database is.

The appsettings file
{
  "ConnectionStrings": {
    "SqlConnection": "Server=.\SQLEXPRESS;Database=AgentBooking; Integrated Security=true;encrypt=false"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

The "SqlConnection": "Server=.\SQLEXPRESS;Database=AgentBooking; Integrated Security=true;encrypt=false" bit needs to be updated accordingly to connect to the database.


# SQL
The sql table scripts and procedure scripts are in the SQL folder. The master data is in the Excel files in the same folder.

Used MSSQL as the backend.
