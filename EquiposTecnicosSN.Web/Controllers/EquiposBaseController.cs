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
                .Where(u => u.NombreCompleto.StartsWith(term))
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

        public void ShowTraslados(int equipoID)
        {

        }


        public void SetViewBagValues(Equipo equipo)
        {
            if (equipo.EquipoId == 0)
            {
                ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "Nombre");
                ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
                ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nombre");
                ViewBag.MarcaId = new SelectList(Enumerable.Empty<Marca>(), "MarcaId", "Nombre");
                ViewBag.ModeloId = new SelectList(Enumerable.Empty<Modelo>(), "ModeloId", "Nombre");
            }
            else
            {
                ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nombre", equipo.InformacionHardware.FabricanteId);
                ViewBag.MarcaId = new SelectList(db.Marcas.Where(m => m.FabricanteId == equipo.InformacionHardware.FabricanteId), "MarcaId", "Nombre", equipo.InformacionHardware.MarcaId);
                ViewBag.ModeloId = new SelectList(db.Modelos.Where(m => m.MarcaId == equipo.InformacionHardware.MarcaId), "ModeloId", "Nombre", equipo.InformacionHardware.ModeloId);
                ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "Nombre", equipo.UbicacionId);
                ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", equipo.InformacionComercial.ProveedorId);
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
