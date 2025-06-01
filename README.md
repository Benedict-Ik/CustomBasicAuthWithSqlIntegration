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
