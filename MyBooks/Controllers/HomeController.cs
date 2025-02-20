using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBooks.Data;
using MyBooks.DTOs;
using MyBooks.Models;
using System.Text.Json;
using MyBooks.Helpers;

namespace MyBooks.Controllers;

public class HomeController : Controller
{
    private readonly MyBooksDbContext _context;

    public HomeController(MyBooksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        User? LoggedInUser = SessionHelper.LoggedInUser(HttpContext);

        if (LoggedInUser is not null)
        {
            ViewData["ID"] = LoggedInUser.UserID;
            ViewData["UserName"] = LoggedInUser.User_Name;
            ViewData["Email"] = LoggedInUser.Email;
            ViewBag.LoggedInStatus = true;
        }
        return View();
    }

    //Login 
    [HttpPost]
    public async Task<IActionResult> Index(LoginDTO loginInfo)
    {
        if (!ModelState.IsValid) 
        {
            return View(loginInfo);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.User_Name == loginInfo.User_Name&& u.Email == loginInfo.Email);
        if (user == null) 
        {
            ModelState.AddModelError(string.Empty, "Felaktigt användarnamn eller email.");
            return View(loginInfo);
        }

        string userSession = JsonSerializer.Serialize(user);
        HttpContext.Session.SetString("Session", userSession);


        return RedirectToAction("Index");
    }
}
