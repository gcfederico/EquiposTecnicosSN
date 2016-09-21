using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Models;
using EquiposTecnicosSN.Web.Services;
using Salud.Security.SSO;
using System.Linq;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
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
            
            SSOHelper.Authenticate();
            if (SSOHelper.CurrentIdentity == null)
            {
                SSOHelper.RedirectToSSOPage("Login.aspx", Request.Url.ToString());
            }
            

            ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
            ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre");
            var vm = new HomeViewModel
            {
                searchEquipo = new SearchEquipoViewModel(),
                searchOdt = new SearchOdtViewModel(),
                CorrectivosCount = odtsService.MCorrectivosAbiertos(OrdenDeTrabajoPrioridad.Emergencia).Count(),
                EquiposFuncionalesCount = equiposService.EquiposFuncionalesCount(),
                PreventivosCount = odtsService.MPreventivosProximos().Count(),
                RepuestosCount = db.Repuestos.Count()
            };

            return View(vm);
        }

        public void Logout()
        {
            Session.Abandon();
            SSOHelper.RedirectToSSOPage("Logout.aspx", Request.Url.ToString());
        }

    }
}