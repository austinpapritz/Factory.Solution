using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Putting SelectList in ViewBag
using Microsoft.EntityFrameworkCore;
using Factory.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Factory.Controllers;

public class MachinesController : Controller
{
    private readonly FactoryContext _db;
    public MachinesController(FactoryContext db)
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
            .Include(m => m.MachineLicenses)
            .ThenInclude(ml => ml.License)
            .FirstOrDefault(m => m.MachineId == id);

        if (model == null)
        {
            return NotFound();
        }

        // Fetch the Machine's Licenses to add qualified engineers to model.
        List<int> machineLicenseIds = model.MachineLicenses.Select(ml => ml.LicenseId).ToList();

        // Get all Engineers with their Licenses.
        var engineers = _db.Engineers
            .Include(e => e.EngineerLicenses)
            .ToList();

        // Add to model Engineers who have every license to work on the machine.
        model.Engineers = engineers
            .Where(e => machineLicenseIds.All(id => e.EngineerLicenses.Any(el => el.LicenseId == id)))
            .ToList();

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Both Create and Edit routes use `Form.cshtml`
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add Machine";

        // Add list of Licenses to ViewBag
        ViewBag.Licenses = _db.Licenses
            .Select(l => new License
            {
                LicenseId = l.LicenseId,
                Name = l.Name,
                IsSelected = false
            })
            .ToList();

        return View("Form");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Country,Make,Model")] Machine machine, List<int> selectedLicenseIds)
    {
        if (ModelState.IsValid)
        {
            // Add selected Licenses to machines's MachineLicenses list.
            machine.MachineLicenses = new List<MachineLicense>();
            foreach (var licenseId in selectedLicenseIds)
            {
                machine.MachineLicenses.Add(new MachineLicense { LicenseId = licenseId });
            }

            // Add machine to Machine's table.
            _db.Machines.Add(machine);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // If anything goes wrong, reload form.
        // Add list of Licenses to ViewBag
        ViewBag.Licenses = _db.Licenses
            .Select(l => new License
            {
                LicenseId = l.LicenseId,
                Name = l.Name,
                IsSelected = false
            })
            .ToList();
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add Machine";
        return View("Form");
    }


}
