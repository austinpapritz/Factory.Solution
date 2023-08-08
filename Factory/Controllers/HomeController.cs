using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Factory.Controllers;

public class HomeController : BaseController
{
    // Pass db to base constructor.
    public HomeController(FactoryContext db) : base(db)
    {
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
