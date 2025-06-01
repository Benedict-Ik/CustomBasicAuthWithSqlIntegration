# Custom Basic Auth 

Here, we created a `Models` folder that will house our `User` model with properties below:
```C#
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
}
```

We also created a `Data` folder that will house our `AppDbContext` class, which extends `IdentityDbContext\<User>`, and contains a `DbSet<User>` property to represent the users in our database.