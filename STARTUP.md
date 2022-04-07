# Setting Up for My2Cents Application
> v2.0.0

Please make sure that you follow all the steps below to make the app be able to run as it is in the <code>LOCALHOST</code>. If you want to run it in cloud, please make sure to add the configurations for the App Keys and ConnectionString. 

All steps here are required to add new information to the <code>appsettings.json</code> file which you need to create in the API folder.

# Table of Contents
- [Setting Up for My2Cents Application](#setting-up-for-my2cents-application)
- [Table of Contents](#table-of-contents)
- [Database Setup](#database-setup)
  - [ConnectionStrings](#connectionstrings)
  - [Create Database](#create-database)
    - [MacOS Users](#macos-users)
    - [WindowsOS Users](#windowsos-users)
- [Access Token For APIs](#access-token-for-apis)
- [SendGrid](#sendgrid)
  - [Create SendGrid Account For Sending Email](#create-sendgrid-account-for-sending-email)
  - [Setup](#setup)

# Database Setup
## ConnectionStrings
In the <code>appsettings.json</code> file, please add these lines below:
```json
"ConnectionStrings": {
    "connectionString": "yourConnectionStringToDatabase"
}
```
In our project, we used SQL Server, if you want to use another database, please change the setting in the <code>Program.cs</code> file too.

## Create Database
> Shell scripts to create database 

In the Backend repository, <code>cd</code> to the ScaffoldModel folder. Ex:
```cmd
cd ./My-2-Cents-API/My2Cents.API/My2Cents.DataInfrastructure/ScaffoldModel
```

Make sure that you already setup the "<code>dotnet ef</code>" tools for these steps, and please check if you have already created the <code>appsettings.json</code> in the API folder for the connection string setup.

Use these command lines below to generate the migration and database update.
### MacOS Users
```cmd
dotnet ef migrations add InitialDatabase --startup-project ../../My2Cents.API

dotnet ef database update --startup-project ../../My2Cents.API
```
### WindowsOS Users
```cmd
dotnet-ef migrations add InitialDatabase --startup-project ../../My2Cents.API

dotnet-ef database update --startup-project ../../My2Cents.API
```

# Access Token For APIs
In the <code>appsettings.json</code> file, please add these lines below:
```json
"Token": {
    "Key": "youCanPutAnyThingHere"
}
```

# SendGrid
## Create SendGrid Account For Sending Email
Please access this website [SendGrid](https://sendgrid.com).

![screenshot](./assests/SendGridWelcomePage.png)
You can start with the Free plan(in our application we are using the Free plan) or register with the other plans.

After creating the account, you will need to fill out all the other information as a requirement from SendGrid

From here, you can click <code>Create a new Single Sender</code>
![screenshot](./assests/CreateSingleSender.png)

Fill out all the information, please make sure that the <code>From Email Address</code> is the Email Address that you will use to send emails. Save this email address for the Setup step.
![screenshot](./assests/CreateASender.png)

Make sure to verify your email.

When you're done with the verification step, there should be a green checkmark next to "Create a sender Identity" and you should see a screen similar to the image below.
![screenshot](./assests/IntergrateWebAPI.png)

On the left sidebar, go to "Settings/API Keys"
and click "Create API Key" to fill out the "API Key Name".
![screenshot](./assests/CreateAnAPIKey.png)

Click "Create & View" and copy this API Key somewhere so you can use it for the setup step.

## Setup
In the <code>appsettings.json</code> file, please add these lines below:
```json
"SendGrid": {
    "My2CentsSendGridEmailAPI": "yourSendGridAPIKey",
    "EmailAddress": "yourEmailRegisteredInSendGrid"
}
```