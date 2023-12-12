```
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

Ctrl+Shift+B - you've added a new field to the Movie class, you need to update the property binding list so this new property will be included. In MoviesController.cs, update the [Bind] attribute for both the Create and Edit action methods to include the Rating property:
```

```
<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.ImageUrl)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Title)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.ReleaseDate)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Genre)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Price)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    <img src="~/movieImg/@item.ImageUrl" />
                    @* @Html.DisplayFor(modelItem => item.ImageUrl) *@
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Title)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.ReleaseDate)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Genre)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Price)
                </td>
                <td>
                    <a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
                    <a asp-action="Details" asp-route-id="@item.Id">Details</a> |
                    <a asp-action="Delete" asp-route-id="@item.Id">Delete</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```