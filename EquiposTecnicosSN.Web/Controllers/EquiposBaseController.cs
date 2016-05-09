using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Equipos.Info;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class EquiposBaseController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: EquiposBase
        public virtual ActionResult Index()
        {
            return View(db.Equipos.ToList());
        }

        // GET
        public ActionResult AutocompleteNombreUMDNS(string term)
        {
            var model =
                db.Umdns
                .Where(u => u.NombreCompleto.Contains(term))
                .Take(6)
                .Select(e => new
                {
                    label = e.NombreCompleto,
                    value = e.Codigo
                });

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        // GET
        public ActionResult AutocompleteCodigoUMDNS(string term)
        {
         var model =
                db.Umdns
                .Where(u => u.Codigo.StartsWith(term))
                .Take(6)
                .Select(e => new
                {
                    label = e.Codigo,
                    value = e.NombreCompleto
                });

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        public void SetViewBagValues(Equipo equipo)
        {
            if (equipo.EquipoId == 0)
            {
                ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
                ViewBag.ProveedorId = new SelectList(db.Proveedores.OrderBy(u => u.Nombre), "ProveedorId", "Nombre");
                ViewBag.FabricanteId = new SelectList(db.Fabricantes.OrderBy(u => u.Nombre), "FabricanteId", "Nombre");
                ViewBag.MarcaId = new SelectList(Enumerable.Empty<Marca>(), "MarcaId", "Nombre");
                ViewBag.ModeloId = new SelectList(Enumerable.Empty<Modelo>(), "ModeloId", "Nombre");
            }
            else
            {
                ViewBag.FabricanteId = new SelectList(db.Fabricantes.OrderBy(u => u.Nombre), "FabricanteId", "Nombre", equipo.InformacionHardware.FabricanteId);
                ViewBag.MarcaId = new SelectList(db.Marcas.OrderBy(u => u.Nombre).Where(m => m.FabricanteId == equipo.InformacionHardware.FabricanteId), "MarcaId", "Nombre", equipo.InformacionHardware.MarcaId);
                ViewBag.ModeloId = new SelectList(db.Modelos.Where(m => m.MarcaId == equipo.InformacionHardware.MarcaId), "ModeloId", "Nombre", equipo.InformacionHardware.ModeloId);
                ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre", equipo.UbicacionId);
                ViewBag.ProveedorId = new SelectList(db.Proveedores.OrderBy(u => u.Nombre), "ProveedorId", "Nombre", equipo.InformacionComercial.ProveedorId);
            }
        }

        // GET: EquiposBase/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipoBase = db.Equipos.Find(id);
            if (equipoBase == null)
            {
                return HttpNotFound();
            }
            return View(equipoBase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
