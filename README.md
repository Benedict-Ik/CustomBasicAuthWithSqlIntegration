# Custom Basic Auth 

Here, inside the `Helpers` folder, we created a custom `BasicAuthAttribute` class. This class does the following:

- Implements Basic Authentication as an ASP.NET Core action filter, validating credentials before allowing access to API endpoints.
- Extracts the Authorization header from incoming requests to check for Basic authentication.
- Decodes Base64-encoded credentials (username and password) and validates them.
- Checks against the database to verify if the provided username exists.
- Uses the `PasswordHelper` class to authenticate passwords securely.
- Logs authentication attempts and failures for debugging and security monitoring.
- Sends a 401 Unauthorized response when authentication fails due to missing headers, invalid format, or incorrect credentials.