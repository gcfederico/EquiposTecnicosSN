﻿using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Equipos.Info;
using EquiposTecnicosSN.Web.Services;
using System.Diagnostics;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class EquiposCirugiaController : EquiposBaseController
    {
        private EquiposDbContext db = new EquiposDbContext();
        private IdentityDb identityDb = new IdentityDb();
        private EquiposService equiposService = new EquiposService();
        // GET: EquiposClimatizacion
        public override ActionResult Index()
        {
            var appuser = identityDb.Users.Where(u => u.UserName == User.Identity.Name).Single();
            var equipos = db.EquiposDeCirugia.ToList();
            return View(equipos);
        }

        // GET: EquiposClimatizacion/Details/5
        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = equiposService.GetEquipo((int) id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: EquiposClimatizacion/Create
        public ActionResult Create()
        {
            var model = new EquipoCirugia();
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
        public ActionResult Create(EquipoCirugia equipoCirugia)
        {
            
            if (ModelState.IsValid)//validaciones
            {
                db.EquiposDeCirugia.Add(equipoCirugia);
                db.SaveChanges();
                ViewBag.CssClass = "success";
                ViewBag.Message = "Equipo creado.";

                return RedirectToAction("Index");
            }

            base.SetViewBagValues(equipoCirugia);
            return View(equipoCirugia);
        }

        // GET: EquiposClimatizacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var equipoCirugia = db.EquiposDeCirugia.Find(id);
            if (equipoCirugia == null)
            {
                return HttpNotFound();
            }
            base.SetViewBagValues(equipoCirugia);

            return View(equipoCirugia);
        }

        // POST: EquiposClimatizacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipoCirugia equipoCirugia)
        {

            if (ModelState.IsValid) //validaciones
            {
                db.Entry(equipoCirugia).State = EntityState.Modified;
                db.Entry(equipoCirugia.InformacionComercial).State = EntityState.Modified;
                db.Entry(equipoCirugia.InformacionHardware).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            base.SetViewBagValues(equipoCirugia);
            return View(equipoCirugia);
        }

        // GET: EquiposClimatizacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var equipoCirugia = db.EquiposDeCirugia.Find(id);
            if (equipoCirugia == null)
            {
                return HttpNotFound();
            }
            return View(equipoCirugia);
        }

        // POST: EquiposClimatizacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipoCirugia equipo = db.EquiposDeCirugia.Find(id);
            db.EquiposDeCirugia.Remove(equipo);
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