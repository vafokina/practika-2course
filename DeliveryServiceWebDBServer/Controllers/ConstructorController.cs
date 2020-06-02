using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryServiceWebDBServer.Controllers
{
    public class ConstructorController : Controller
    {
        // GET: Constructor
        public ActionResult Index()
        {
            TempData["ActiveConstructor"] = true;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string[] table)
        {
            TempData["ActiveConstructor"] = true;
            Models.Constructor c = new Models.Constructor { SelectedTables = table };
            c.Attributes=c.GetAttributes();
            return View("Index2", c);
        }
        [HttpPost]
        public ActionResult Index2(Models.Constructor c)
        {
            TempData["ActiveConstructor"] = true;
            c.References = c.GetReferences();
            return View("Index3", c);
        }
        [HttpPost]
        public ActionResult Index3(Models.Constructor c)
        {
            TempData["ActiveConstructor"] = true;
            return View("Index4", c);
        }
    }
}