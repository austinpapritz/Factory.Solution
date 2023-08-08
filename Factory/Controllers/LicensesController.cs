using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Factory.Controllers;

public class LicensesController : BaseController
{
    // Pass db to base constructor.
    public LicensesController(FactoryContext db) : base(db)
    {
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add License";

        return View("Form");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Name")] License license)
    {
        if (ModelState.IsValid)
        {
            // Add license to Licenses table.
            _db.Licenses.Add(license);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // If anything goes wrong, reload form.

        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add License";
        return View("Form");
    }

}