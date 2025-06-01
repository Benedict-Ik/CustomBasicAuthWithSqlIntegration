# Custom Basic Auth 

- Created an `AuthController` class to handle basic authentication for the application with `register` and `login` endpoints. 
- Created an `AuthDto` class in the `Models` folder to handle the data transfer object for authentication requests.
- Also in `Program.cs`, we added the below lines of code:
 
```csharp
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PasswordHashHelper>();
```

- Replaced the `.AddSwaggerGen()` method with the below:

```csharp
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Enter 'Basic' [space] and then your generated encoded Base64 credentialsn.\n\nExample: Basic dXNlcm5hbWU6cGFzc3dvcmQ='",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Basic"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type=ReferenceType.SecurityScheme,
                    Id="Basic"
                }
            },
            new string[]{}
        }
    });
});
```