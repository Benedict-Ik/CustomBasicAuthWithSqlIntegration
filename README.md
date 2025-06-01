# Custom Basic Auth 

Here, we created a new folder called `Helpers` which will house our newly created `PasswordHashHelper` class. This class will be responsible for:
- Handling password security using the `PasswordHasher<User>` class.
- Verifying passwords by comparing input passwords with stored hashed passwords.
- Using the User object for password hashing and verification.
- Returning true for successful password matches, ensuring secure authentication.