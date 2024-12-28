
## Tech Stack

**Database:** SQL Server, MongoDB

**Skill:** C#, ASP.Net Core Web API, JWT, AutoMapper...

**Framework:** AspNetCore




## Documentation

**Step 1: Download project**

Open terminal/cmd and navigate to the path where you want to download the project and and run the following command

```bash
  git clone https://github.com/tquocan04/Online_Shopping.git
```
or click on Code to download zip file

![Download ZIP]("https://drive.google.com/file/d/1CnkPWVIswMmcif1cu-E9fHcXrjqjJKZs/view")


**Step 2: Create a server in SQL Server**

If you don't have SQL server already, you'll need to install it. You can choose platform and edition that suits your needs.
[Install SQL server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

Then you click on the downloaded file and select the basic type to create a server.

**Step 3: Create a new database**

If you don't have SQL Server Management Studio (SSMS) already, you'll need to install it. 
[Install SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms)

Then you click on the downloaded file and select the basic type to create a server.

Open SSMS, select *Window Authentication* and connect to your server.

Next, you choose *New Query* (or ctrl+N) to create a new query file.

![](https://drive.google.com/uc?id=1_fu8GHeyzp1KWF4-FeGgKZ-xSYDns1KB)

After that, you will enter:

```bash
  create database Online_Shopping
```

to create a new database.

**Step 4: Connect database to project**

Open project in Visual Studio IDE

Link to download Visual Studio IDE (Visual Studio 2022 - Community edition):[Download Visual Studio IDE](https://visualstudio.microsoft.com/downloads/)

If your platform is MacOS or Linux, you can download Visual Studio Code:
[Download Visual Studio Code](https://visualstudio.microsoft.com/downloads/). You also need to install many extensions to run a .Net Core Web API project.

In SSMS, press F4 to copy name of Server Name.

In toolbar of Visual Studio 2022, you will select View -> Server Explorer (ctrl+alt+s) and click on *Connect to Database* icon. 

![Connect to Database icon]("https://drive.google.com/file/d/1YNtc6zeLg0Iq9E6kq5vHaDwDdfK28rBH/view")

Next, you need to paste the server name in Server Name. Then you choose log on method similar to the way you log in to server in SSMS and select database name *Online_Shopping* which is created and click OK to connect.

**Step 5: Configure .env file**

Firstly, you will open Server Explorer and right click on database is connected -> choose properties -> copy information of Connection String.

![Connection String]("https://drive.google.com/file/d/19l4YFqp7DhaZ0MgZXP_MuvwrHak6wXFv/view?usp=drive_link")

Secondly, you right click on Online_Shopping project(not solution) -> choose *New item* -> enter *.env* to create .env file.

Thirdly, in the .env file, you will enter:

```bash
  DB_CONNECTION_STRING=<Your_Connect_String>
```

If there is password, you need to enter the correct server password.

**Step 6: Connect to MongoDB**

Firstly, if you  don't have MongoDB already, you need to download it: [Download MongoDBCompass](https://www.mongodb.com/try/download/compass)

Secondly, open MongoDBCompass, you will add new connection. In this connection, you need to create a new database name *Online_Shopping* and collection name *customer* and *product*.

![Database in MongoDB]("https://drive.google.com/file/d/1WCUeKiEvVMeNGqBehtAQv2wYfqCfR82J/view?usp=drive_link")

Thirdly, open *application.json* file in *Online_Shopping* project, change information of *DbConnection* of MongoDB configuration to your MongoDB information.
## Running Project

Press F5 to run project.

