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
using System.Data.Entity.Validation;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class EquiposClimatizacionController : Controller
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: EquiposClimatizacion
        public ActionResult Index()
        {
            var equipos = db.EquiposDeClimatizacion.Include(e => e.InformacionComercial).Include(e => e.Ubicacion).Include(e => e.HistorialDeMantenimientos);
            return View(equipos.ToList());
        }

        // GET: EquiposClimatizacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoClimatizacion equipoClimatizacion = db.EquiposDeClimatizacion.Find(id);
            if (equipoClimatizacion == null)
            {
                return HttpNotFound();
            }
            return View(equipoClimatizacion);
        }

        // GET: EquiposClimatizacion/Create
        public ActionResult Create()
        {
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto");
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            var model = new EquipoClimatizacion();
            model.InformacionComercial = new InformacionComercial();
            return View(model);
        }

        // POST: EquiposClimatizacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoId,NombreCompleto,UMDNS,Tipo,NumeroSerie,Modelo,NumeroInventario,UbicacionId,Estado,ProveedorId,InformacionComercial")] EquipoClimatizacion equipoClimatizacion)
        {
            
            try
            {
                if (true)//validaciones
                {
                    var infCom = equipoClimatizacion.InformacionComercial;
                    equipoClimatizacion.InformacionComercial = null;
                    infCom.Proveedor = db.Proveedores.Find(infCom.ProveedorId);
                    
                    db.EquiposDeClimatizacion.Add(equipoClimatizacion);
                    db.SaveChanges();

                    infCom.Equipo = equipoClimatizacion;
                    db.InformacionesComerciales.Add(infCom);
                    db.SaveChanges();
                }   
            } catch (DbEntityValidationException e)
            {
                var errores = e.EntityValidationErrors;
                ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", equipoClimatizacion.UbicacionId);
                ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", equipoClimatizacion.InformacionComercial.ProveedorId);
                return View(equipoClimatizacion);

            }

                return RedirectToAction("Index");

        }

        // GET: EquiposClimatizacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoClimatizacion equipoClimatizacion = db.EquiposDeClimatizacion.Find(id);
            if (equipoClimatizacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipoId = new SelectList(db.InformacionesComerciales, "EquipoId", "NotasGarantia", equipoClimatizacion.EquipoId);
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", equipoClimatizacion.UbicacionId);
            ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", equipoClimatizacion.InformacionComercial.ProveedorId);
            return View(equipoClimatizacion);
        }

        // POST: EquiposClimatizacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoId,NombreCompleto,UMDNS,Tipo,NumeroSerie,Modelo,NumeroInventario,UbicacionId,Estado,InformacionComercial")] EquipoClimatizacion equipoClimatizacion)
        {

            try
            {
                if (true) //validaciones
                {
                    db.Entry(equipoClimatizacion).State = EntityState.Modified;
                    equipoClimatizacion.InformacionComercial = db.InformacionesComerciales.Find(equipoClimatizacion.EquipoId);
                    equipoClimatizacion.InformacionComercial.Proveedor = db.Proveedores.Find(equipoClimatizacion.InformacionComercial.ProveedorId);
                    db.Entry(equipoClimatizacion.InformacionComercial).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                var errores = e.EntityValidationErrors;

                ViewBag.EquipoId = new SelectList(db.InformacionesComerciales, "EquipoId", "NotasGarantia", equipoClimatizacion.EquipoId);
                ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", equipoClimatizacion.UbicacionId);
                ViewBag.ProveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre", equipoClimatizacion.InformacionComercial.ProveedorId);
                return View(equipoClimatizacion);
            }


        }

        // GET: EquiposClimatizacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoClimatizacion equipoClimatizacion = db.EquiposDeClimatizacion.Find(id);
            if (equipoClimatizacion == null)
            {
                return HttpNotFound();
            }
            return View(equipoClimatizacion);
        }

        // POST: EquiposClimatizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipoClimatizacion equipoClimatizacion = db.EquiposDeClimatizacion.Find(id);
            db.EquiposDeClimatizacion.Remove(equipoClimatizacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET
        public ActionResult Autocomplete(string term)
        {
            var model =
                db.EquiposDeClimatizacion
                .Where(e => e.NombreCompleto.StartsWith(term))
                .Take(6)
                .Select(e => new
                {
                    label = e.NombreCompleto
                });

            return Json(model, JsonRequestBehavior.AllowGet);
            
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
