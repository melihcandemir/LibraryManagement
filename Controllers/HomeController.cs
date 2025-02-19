using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

public class HomeController : Controller
{
    // the method that calls the first page that appears when the project is opened
    public IActionResult Index()
    {
        return View();
    }


    // method that calls the about page
    public IActionResult Privacy()
    {
        return View();
    }
}
