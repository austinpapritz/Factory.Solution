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

        // Get all Engineers with their EngineerLicenses.
        List<Engineer> engineers = _db.Engineers
            .Include(e => e.EngineerLicenses)
            .ToList();

        // This filters out the engineers that have every license necessary to work on the machine.
        // For each engineer, we are checking if all the machine's licenses are contained in the engineer's licenses.
        List<Engineer> qualifiedEngineers = engineers
            .Where(e => machineLicenseIds.All(ml => e.EngineerLicenses.Any(el => el.LicenseId == id)))
            .ToList();

        // This assigns the list of qualified engineers to the machine.
        model.Engineers = qualifiedEngineers;


        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Both Create and Edit routes use `Form.cshtml`
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add Machine";

        // Add list of Licenses to ViewBag.
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

    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Fetch machine and included their MachineLicenses.
        Machine machineToBeEdited = _db.Machines
            .Include(m => m.MachineLicenses)
            .FirstOrDefault(m => m.MachineId == id);

        if (machineToBeEdited == null)
        {
            return NotFound();
        }

        // Fetch all licenses.
        List<License> licenses = _db.Licenses.ToList();

        // Mark the licenses that the machine already has. Add licenses list to ViewBag.
        foreach (var license in licenses)
        {
            // "Set `IsSelected` true for each of the machine's licenses and false for all the others. This is to render check boxes.
            // `?. [..] ?? false` covers if `MachineLincenses` is null, it'll just set every `license.IsSelected` to `false`.
            license.IsSelected = machineToBeEdited.MachineLicenses?.Any(ml => ml.LicenseId == license.LicenseId) ?? false;
        }
        ViewBag.Licenses = licenses;



        // Both Create and Edit routes use `Form.cshtml`.
        ViewData["FormAction"] = "Edit";
        ViewData["SubmitButton"] = "Update Machine";

        return View("Form", machineToBeEdited);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("MachineId,Country,Make,Model")] Machine machine, List<int> selectedLicenseIds)
    {
        // Ensure id from form and url match.
        if (id != machine.MachineId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            try
            {
                // Load the machine from the database, including the current licenses.
                var dbMachine = _db.Machines
                    .Include(m => m.MachineLicenses)
                    .Single(m => m.MachineId == id);

                // Update machine fields.
                dbMachine.Country = machine.Country;
                dbMachine.Make = machine.Make;
                dbMachine.Model = machine.Model;

                // Clear the current licenses and add the selected ones.
                // REFACTOR THIS SO THAT YOU DON'T NEED TO CLEAR OLD LIST.
                dbMachine.MachineLicenses.Clear();
                foreach (var licenseId in selectedLicenseIds)
                {
                    dbMachine.MachineLicenses.Add(new MachineLicense { LicenseId = licenseId });
                }

                _db.Update(dbMachine);
                _db.SaveChanges();
            }
            // Catch any ConcurrencyExceptions.
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineExists(machine.MachineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("details", "machines", new { id = machine.MachineId });
        }

        // Otherwise reload form.
        ViewData["FormAction"] = "Edit";
        ViewData["SubmitButton"] = "Update Machine";
        return RedirectToAction("edit", new { id = machine.MachineId });
    }

    // Handled by site.js.
    [HttpPost]
    public IActionResult Delete(int id)
    {
        Machine machineToBeDeleted = _db.Machines.FirstOrDefault(m => m.MachineId == id);

        if (machineToBeDeleted == null)
        {
            return NotFound();
        }

        _db.Machines.Remove(machineToBeDeleted);
        _db.SaveChanges();

        // Return HTTP 200 OK to AJAX request, signalling successful deletion.
        return Ok();
    }

    // Method to validate model in db.
    private bool MachineExists(int id)
    {
        return _db.Machines.Any(m => m.MachineId == id);
    }
}
