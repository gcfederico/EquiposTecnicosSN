using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Models;
using EquiposTecnicosSN.Web.Services;
using System.Linq;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();
        /// <summary>
        /// 
        /// </summary>
        protected ODTsService odtsService = new ODTsService();
        /// <summary>
        /// 
        /// </summary>
        protected EquiposService equiposService = new EquiposService();

        // POST: EquiposBase/EquiposDeUsuarioCount
        [HttpPost]
        public JsonResult EquiposDeUsuarioCount()
        {
            //Get ubicacion de Usuario
            var count = db.Equipos.Count();
            return Json(count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Index View Model</returns>
        public ActionResult Index()
        {

            ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
            ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre");
            var vm = new HomeViewModel
            {
                searchEquipo = new SearchEquipoViewModel(),
                searchOdt = new SearchOdtViewModel(),
                CorrectivosCount = odtsService.CorrectivosAbiertosCount(),
                EquiposFuncionalesCount = equiposService.EquiposFuncionalesCount(),
                PreventivosCount = odtsService.PreventivosAbiertosCount(),
                RepuestosCount = db.Repuestos.Count()
            };

            return View(vm);
        }

    }
}