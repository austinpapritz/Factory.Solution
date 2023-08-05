using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Putting SelectList in ViewBag
using Microsoft.EntityFrameworkCore;
using Factory.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Factory.Controllers;

public class HomeController : Controller
{
    private readonly FactoryContext _db;
    public HomeController(FactoryContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        List<Engineer> engineers = _db.Engineers.ToList();
        List<Machine> machines = _db.Machines.ToList();

        ViewBag.Engineers = engineers;
        ViewBag.Machines = machines;
        return View();
    }

}
