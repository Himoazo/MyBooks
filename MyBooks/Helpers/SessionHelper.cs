using MyBooks.Models;
using System.Text.Json;

namespace MyBooks.Helpers;

public class SessionHelper
{
    public static User? LoggedInUser (HttpContext httpContext)
    {
        string? userSession = httpContext.Session.GetString("Session");

        return userSession != null ? JsonSerializer.Deserialize<User?>(userSession) : null;
    }

}


