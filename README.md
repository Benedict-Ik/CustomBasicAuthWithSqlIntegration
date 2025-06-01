# Custom Basic Auth 

- Here, we created a `Data` folder that will house our `AppDbContext` class, and contains a `DbSet<User>` property to represent the users in our database.


- Next, we added the below commands to run the migrations and update the database via athe package manager console:
```bash
Add-Migration InitialCreate
Update-Database
```