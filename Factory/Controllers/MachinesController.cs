using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Putting SelectList in ViewBag
using Microsoft.EntityFrameworkCore;
using Factory.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Factory.Controllers;

public class EngineersController : Controller
{
    private readonly FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
        _db = db;
    }

    public ActionResult Index()
    {
        List<Machine> model = _db.Machines.ToList();
        return View(model);
    }

    public IActionResult Details(int id)
    {
        Machine model = _db.Machines
            .Include(e => e.MachineLicenses)
            .ThenInclude(el => el.License)
            .FirstOrDefault(e => e.MachineId == id);

        if (model == null)
        {
            return NotFound();
        }

        // Fetch the Machine's Licenses to add qualified engineers to model.
        List<int> machineLicenseIds = model.MachineLicenses.Select(ml => ml.LicenseId).ToList();

        // Add to model Engineers qualified to work on machine
        model.Engineers = _db.Engineers
            .Include(e => e.EngineerLicenses)
            .Where(e => e.EngineerLicenses.All(el => machineLicenseIds.Contains(el.LicenseId)))
            .ToList();

        return View(model);
    }

}
