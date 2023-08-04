using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Diagnostics;

namespace Factory.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

}
