using EquiposTecnicosSN.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class SolicitudesRepuestoServicioController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: SolicitudesRepuestoServicio/DetailsSolicitud
        [HttpGet]
        public ActionResult DetailsSolicitud(int id)
        {
            var solicitud = db.SolicitudesRepuestosServicios.Find(id);
            return PartialView("_DetailSolicitudRepuestoServicioContent", solicitud);
        }


    }
}