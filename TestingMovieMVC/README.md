The Model-View-Controller (MVC) architectural pattern separates an app into three main components: Model, View, and Controller. The MVC pattern helps you create apps that are more testable and easier to update than traditional monolithic apps.

View templates should not:
-Do business logic
-Interact with a database directly.

.netDeveloper
-crud, -configuration, -designPattern(oop), -client side(razorblade), -database(msssql, mysql), -database(core level, efcore), -Linq(), -caching(software), -loggin(web server)

Scaffolding creates the following:
-A movies controller: Controllers/MoviesController.cs
-Razor view files for Create, Delete, Details, Edit, and Index pages: -Views/Movies/*.cshtml
-A database context class: Data/MvcMovieContext.cs

5.12 (error) and migration
-create a new database in MSSQL 
-initialCatalog မှာ database name
-'TrustServerCertificate=True' in appsetting.json [connectionStrings]
-Data Source = "." or locathost