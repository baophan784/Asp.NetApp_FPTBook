using FPTBookApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FPTBookApp2.Controllers
{
    public class FooController : Controller
    {
        // GET: Foo
        private FPTdbEntities db = new FPTdbEntities();
        [ChildActionOnly]
        public ActionResult Nav()
        {
            return PartialView("_Nav", db.categories.ToList());
        }

        
    }
}