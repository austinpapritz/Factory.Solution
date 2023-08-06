using Microsoft.AspNetCore.Mvc;
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
        List<Engineer> model = _db.Engineers.ToList();
        return View(model);
    }

    public IActionResult Details(int id)
    {
        // Get the engineer with the `EngineerId` that matches the url id, 
        // include the `EngineerLicenses`, then include each corresponding `License`.
        Engineer model = _db.Engineers
            .Include(e => e.EngineerLicenses)
            .ThenInclude(el => el.License)
            .FirstOrDefault(e => e.EngineerId == id);

        if (model == null)
        {
            return NotFound();
        }

        // Get the engineer's `EngineerLicenses` and assign the corresponding `LicenseId`s to a HashSet.
        // .Contains() directly finds items in HashSet, whereas a List must be searched linearly.
        HashSet<int> licenseIdsForEngineer = model.EngineerLicenses.Select(el => el.LicenseId).ToHashSet();

        // This filters out the machines that the engineer is licensed to repair. For every machine get their 
        // list of `MachineLicenses`. See if the `LicenseId`s in the `MachineLicenses` all have  matching 
        // `LicenseId`s in the Engineer's list of `EngineerLicenses` (which we assigned to `licenseIdsForEngineer`above).
        List<Machine> qualifiedMachines = _db.Machines
            .Include(m => m.MachineLicenses)
            .Where(m => m.MachineLicenses.All(ml => licenseIdsForEngineer.Contains(ml.LicenseId)))
            .ToList();

        // This assigns the list of machines that the engineer can repair to the model.
        model.Machines = qualifiedMachines;

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Both Create and Edit routes use `Form.cshtml`
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add Engineer";

        // Add list of `Licenses` to ViewBag, setting `IsSelected = false' so that the license checklist start unchecked.
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
    public IActionResult Create([Bind("Name")] Engineer engineer, List<int> selectedLicenseIds)
    {
        if (ModelState.IsValid)
        {
            // Create a new `EngineerLicenses` HashSet to populate with selected licenses.
            engineer.EngineerLicenses = new HashSet<EngineerLicense>();
            // Add the selected licenses to engineer's `EngineerLicenses` list.
            foreach (int licenseId in selectedLicenseIds)
            {
                // Create a new `EngineerLicense` for every `License` selected, assign the `LicenseId` to it.
                engineer.EngineerLicenses.Add(new EngineerLicense { LicenseId = licenseId });
            }

            // Add engineer to Engineers table.
            _db.Engineers.Add(engineer);
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
        ViewData["SubmitButton"] = "Add Engineer";
        return View("Form");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        // Fetch engineer and included their `EngineerLicenses`.
        Engineer engineerToBeEdited = _db.Engineers
            .Include(e => e.EngineerLicenses)
            .FirstOrDefault(e => e.EngineerId == id);

        if (engineerToBeEdited == null)
        {
            return NotFound();
        }

        // Fetch all licenses.
        List<License> licenses = _db.Licenses.ToList();

        // Mark the licenses that the engineer already has. Add licenses to `ViewBag`.
        foreach (var license in licenses)
        {
            // "Set `IsSelected` true for each of the engineer's licenses and false for all the others. This is to render check boxes.
            // `?. [..] ?? false` covers if `EngineerLincenses` is null, it'll just set every `license.IsSelected` to `false`.
            license.IsSelected = engineerToBeEdited.EngineerLicenses?.Any(el => el.LicenseId == license.LicenseId) ?? false;
        }
        ViewBag.Licenses = licenses;



        // Both Create and Edit routes use `Form.cshtml`.
        ViewData["FormAction"] = "Edit";
        ViewData["SubmitButton"] = "Update Engineer";

        return View("Form", engineerToBeEdited);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("EngineerId,Name")] Engineer engineer, List<int> selectedLicenseIds)
    {
        // Ensure id from form and url match.
        if (id != engineer.EngineerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Load the engineer from the database, including the current licenses.
                var dbEngineer = _db.Engineers
                    .Include(e => e.EngineerLicenses)
                    .Single(e => e.EngineerId == id);

                // Update the name.
                dbEngineer.Name = engineer.Name;

                // Clear the current licenses and add the selected ones.
                dbEngineer.EngineerLicenses.Clear();
                foreach (var licenseId in selectedLicenseIds)
                {
                    dbEngineer.EngineerLicenses.Add(new EngineerLicense { LicenseId = licenseId });
                }

                _db.Update(dbEngineer);
                _db.SaveChanges();
            }
            // Catch any ConcurrencyExceptions.
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineerExists(engineer.EngineerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("details", "engineers", new { id = engineer.EngineerId });
        }

        // Otherwise, reload form.
        ViewData["FormAction"] = "Edit";
        ViewData["SubmitButton"] = "Update Engineer";
        return RedirectToAction("edit", new { id = engineer.EngineerId });
    }

    // Handled by site.js.
    [HttpPost]
    public IActionResult Delete(int id)
    {
        Engineer engineerToBeDeleted = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);

        if (engineerToBeDeleted == null)
        {
            return NotFound();
        }

        _db.Engineers.Remove(engineerToBeDeleted);
        _db.SaveChanges();

        // Return HTTP 200 OK to AJAX request, signalling successful deletion.
        return Ok();
    }

    // Method to validate model in db.
    private bool EngineerExists(int id)
    {
        return _db.Engineers.Any(e => e.EngineerId == id);
    }
}
