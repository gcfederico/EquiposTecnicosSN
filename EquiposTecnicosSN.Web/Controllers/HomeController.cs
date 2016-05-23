using EquiposTecnicosSN.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // POST: EquiposBase/EquiposDeUsuarioCount
        [HttpPost]
        public JsonResult EquiposDeUsuarioCount()
        {
            //Get ubicacion de Usuario
            var count = db.Equipos.Count();
            return Json(count);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}