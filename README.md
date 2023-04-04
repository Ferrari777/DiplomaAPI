# DiplomaAPI

The project of Web API for diploma.

Steps for creating database locally:

1. VS Studio 2022 and MS SQL Server with SQL Management Studio are installed.
2. Check that your SQL Server is working using SQL Server Configuration Manager. Enable all services if needed.
3. Under "View" in nav bar of VS Studio, find SQL Server Object Explorer and connect to your local SQL server. Then go to the Properties and use value in the "Connection string" field.
4. In VS Studio open appsettings.json file and change SQL Server Connection String for yours. Don't delete "Initial Catalog" parameter and enter the name of database.
5. In VS Studio under "Tools" - "NuGET" open "Package Manager Console".
6. Use existing migration files for creating database: 
```
Add-Migration FixBugInContext
Update-Database
```
7. Open SQL Server Object Explorer and refresh your SQL Server. Then check that new database with tables Users and DocFiles were added.
