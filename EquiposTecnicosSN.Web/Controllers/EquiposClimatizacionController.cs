using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Equipos.Info;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class EquiposClimatizacionController : EquiposBaseController
    {
        private EquiposDbContext db = new EquiposDbContext();
        private IdentityDb identityDb = new IdentityDb();

        // GET: EquiposClimatizacion
        public override ActionResult Index()
        {
            var appuser = identityDb.Users.Where(u => u.UserName == User.Identity.Name).Single();
            IQueryable<EquipoClimatizacion> equipos;
            if (appuser.UbicacionId == 0)
            {
                equipos = db.EquiposDeClimatizacion.Include(e => e.InformacionComercial).Include(e => e.Ubicacion).Include(e => e.HistorialDeMantenimientos);
            } else
            {
                equipos = db.EquiposDeClimatizacion
                    .Include(e => e.InformacionComercial)
                    .Include(e => e.Ubicacion)
                    .Include(e => e.HistorialDeMantenimientos)
                    .Where(e => e.UbicacionId == appuser.UbicacionId);
            }
            return View(equipos.ToList());
        }

        // GET: EquiposClimatizacion/Details/5
        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipoClimatizacion equipoClimatizacion = db.EquiposDeClimatizacion.Include(e => e.Traslados).Where(e => e.EquipoId == id).Single();
            if (equipoClimatizacion == null)
            {
                return HttpNotFound();
            }
            return View(equipoClimatizacion);
        }

        // GET: EquiposClimatizacion/Create
        public ActionResult Create()
        {
            var model = new EquipoClimatizacion();
            model.InformacionComercial = new InformacionComercial();
            model.InformacionHardware = new InformacionHardware();
            base.SetViewBagValues(model);
            return View(model);
        }

        // POST: EquiposClimatizacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipoId,NombreCompleto,UMDNS,Tipo,NumeroMatricula,NumeroInventario,UbicacionId,Estado,ProveedorId,InformacionComercial,InformacionHardware")] EquipoClimatizacion equipoClimatizacion)
        {
            
            if (ModelState.IsValid)//validaciones
            {
                db.EquiposDeClimatizacion.Add(equipoClimatizacion);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            base.SetViewBagValues(equipoClimatizacion);
            return View(equipoClimatizacion);
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
            base.SetViewBagValues(equipoClimatizacion);

            return View(equipoClimatizacion);
        }

        // POST: EquiposClimatizacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipoId,NombreCompleto,UMDNS,Tipo,NumeroMatricula,NumeroInventario,UbicacionId,Estado,InformacionComercial,InformacionHardware")] EquipoClimatizacion equipoClimatizacion)
        {

            if (ModelState.IsValid) //validaciones
            {
                db.Entry(equipoClimatizacion).State = EntityState.Modified;
                db.Entry(equipoClimatizacion.InformacionComercial).State = EntityState.Modified;
                db.Entry(equipoClimatizacion.InformacionHardware).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            base.SetViewBagValues(equipoClimatizacion);
            return View(equipoClimatizacion);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                identityDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
