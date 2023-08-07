using Factory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FactoryContext _db;

        public BaseController(FactoryContext db)
        {
            _db = db;
        }

        // Method to validate model in db.
        protected bool EngineerExists(int id)
        {
            return _db.Engineers.Any(e => e.EngineerId == id);
        }


        // Method to validate model in db.
        protected bool MachineExists(int id)
        {
            return _db.Machines.Any(m => m.MachineId == id);
        }
    }
}