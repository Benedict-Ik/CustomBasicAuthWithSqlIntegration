# Custom Basic Auth 

- Here, we created a `Data` folder that will house our `AppDbContext` class, and contains a `DbSet<User>` property to represent the users in our database.

- Registered the `AppDbContext` in the `Program.cs` file using the `AddDbContext` method, which allows us to inject the database context into our controllers.
```C#
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

- Next, we added the below commands to run the migrations and update the database via athe package manager console:
```bash
Add-Migration InitialCreate
Update-Database
```