# Custom Basic Auth 

Here, inside the `Helpers` folder, we created a custom `BasicAuthAttribute` class. This class does the following:

- Implements Basic Authentication using an ASP.NET Core Action Filter to validate credentials before processing requests.
- Extracts the Authorization header from incoming HTTP requests to check for Basic authentication.
- Decodes Base64-encoded credentials (username and password) from the request header.
- Validates credentials against a database, ensuring the user exists and their password matches the stored hash.
- Uses the `PasswordHashHelper` class to securely verify passwords.
- Logs authentication attempts (successful and failed) for monitoring and debugging.
- Rejects unauthorized access by returning a 401 Unauthorized response if authentication fails.