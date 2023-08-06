using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Factory.Controllers;

public class LicensesController : Controller
{
    private readonly FactoryContext _db;
    public LicensesController(FactoryContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["FormAction"] = "Create";
        ViewData["SubmitButton"] = "Add License";

        return View("Form");
    }

}