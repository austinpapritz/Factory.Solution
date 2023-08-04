using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Putting SelectList in ViewBag
using Microsoft.EntityFrameworkCore;
using Factory.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Factory.Controllers;

public class FactoryController : Controller
{
    private readonly FactoryContext _db;
    public FactoryController(FactoryContext db)
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
        Engineer model = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    public IActionResult Create()
    {
        // Both Create and Edit routes use `Form.cshtml`
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add Engineer";
        return View("Form");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Name")] Engineer engineer)
    {
        if (ModelState.IsValid)
        {
            _db.Engineers.Add(engineer);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add Engineer";
        return View("Form");
    }

    public IActionResult Edit(int id)
    {
        Engineer engineerToBeEdited = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);

        if (engineerToBeEdited == null)
        {
            return NotFound();
        }

        // Both Create and Edit routes use `Form.cshtml`.
        ViewData["FormAction"] = "Edit";
        ViewData["SubmitButton"] = "Update Engineer";

        return View("Form", engineerToBeEdited);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("EngineerId,Name")] Engineer engineer)
    {
        // Ensure id from form and url match.
        if (id != engineer.EngineerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            // Try to update changes, catch any ConcurrencyExceptions.
            try
            {
                _db.Update(engineer);
                _db.SaveChanges();
            }
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

        // Otherwise reload form.
        ViewData["FormAction"] = "Edit";
        ViewData["SubmitButton"] = "Update Engineer";
        return RedirectToAction("edit", new { id = engineer.EngineerId });
    }

    // Method to validate model in db.
    private bool EngineerExists(int id)
    {
        return _db.Engineers.Any(e => e.EngineerId == id);
    }
}
