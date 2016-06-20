using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class ChecklistsMantenimientoPreventivoController : Controller
    {
        protected EquiposDbContext db = new EquiposDbContext();

        // GET: ChecklistsMantenimientoPreventivo
        public ActionResult Index()
        {
            var model = db.ChecklistsMantenimientoPreventivo.ToList();
            return View(model);
        }

        // GET: ChecklistsMantenimientoPreventivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public FileResult Download(int id)
        {
            var checklist = db.ChecklistsMantenimientoPreventivo.Find(id);

            if (checklist != null)
            {
                return File(checklist.Content, checklist.ContentType, checklist.Nombre + checklist.FileExtension);
            }

            return null;
        }

        // GET: ChecklistsMantenimientoPreventivo/Create
        public ActionResult Create()
        {
            var model = new ChecklistMantenimientoPreventivo();
            return View(model);
        }

        // POST: ChecklistsMantenimientoPreventivo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChecklistMantenimientoPreventivo checklist, HttpPostedFileBase checklistFile)
        {
            try
            {

                if (checklistFile != null && checklistFile.ContentLength > 0)
                {
                    var checklistMP = new ChecklistMantenimientoPreventivo
                    {
                        Nombre = checklist.Nombre,
                        FileExtension = Path.GetExtension(checklistFile.FileName),
                        ContentType = checklistFile.ContentType
                    };
                    using (var reader = new BinaryReader(checklistFile.InputStream))
                    {
                        checklistMP.Content = reader.ReadBytes(checklistFile.ContentLength);
                    }

                    db.ChecklistsMantenimientoPreventivo.Add(checklistMP);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "No se pueden grabar los datos. Reinténtelo, y si el problema persiste comuniquese con su administrador.");
            }
            return View(checklist);
        }

        // GET: ChecklistsMantenimientoPreventivo/Edit/5
        public ActionResult Edit(int id)
        {
            var checklistMP = db.ChecklistsMantenimientoPreventivo.Find(id);
            if (checklistMP == null)
            {
                return HttpNotFound();
            }
            return View(checklistMP);
        }

        // POST: ChecklistsMantenimientoPreventivo/Edit/5
        [HttpPost]
        public ActionResult Edit(ChecklistMantenimientoPreventivo checklist, HttpPostedFileBase checklistFile)
        {
            try
            {

                var checklistMP = db.ChecklistsMantenimientoPreventivo.Find(checklist.ChecklistMantenimientoPreventivoId);
                checklistMP.Nombre = checklist.Nombre;

                if (checklistFile != null && checklistFile.ContentLength > 0)
                {
                    checklistMP.FileExtension = Path.GetExtension(checklistFile.FileName);
                    checklistMP.ContentType = checklistFile.ContentType;

                    using (var reader = new BinaryReader(checklistFile.InputStream))
                    {
                        checklistMP.Content = reader.ReadBytes(checklistFile.ContentLength);
                    }
                }

                db.Entry(checklistMP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "No se pueden grabar los datos. Reinténtelo, y si el problema persiste comuniquese con su administrador.");
            }
            return View(checklist);
        }

        // GET: ChecklistsMantenimientoPreventivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChecklistsMantenimientoPreventivo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
