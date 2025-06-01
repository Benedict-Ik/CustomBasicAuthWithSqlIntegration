# Custom Basic Auth 

## Packages Needed


1. **Microsoft.AspNetCore.Identity.EntityFrameworkCore (v 8.0.16)**

1. **Microsoft.EntityFrameworkCore.Tools (v 8.0.16)**  
This package provides tools for Entity Framework Core, allowing you to run migrations and manage your database schema.

1. **Microsoft.EntityFrameworkCore.SqlServer (v 8.0.16)**  
This package is the SQL Server database provider for Entity Framework Core, enabling you to interact with SQL Server databases.

## Using Package Manager Console to install packages
```bash
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
```