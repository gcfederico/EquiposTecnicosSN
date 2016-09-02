using EquiposTecnicosSN.Web.DataContexts;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public class StockRepuestosController : Controller
    {
        EquiposDbContext db = new EquiposDbContext();
        // GET: StockRepuestos
        public ActionResult Index(string searchCodigoRepuesto = null, int repuestoId = 0, int proveedorId = 0, int page = 1)
        {
            var listPage = db.StockRepuestos
                .Where(sr => searchCodigoRepuesto == null || sr.Repuesto.Codigo.Contains(searchCodigoRepuesto))
                .Where(sr => proveedorId == 0 || sr.Repuesto.ProveedorId == proveedorId)
                .Where(sr => repuestoId == 0 || sr.Repuesto.RepuestoId == repuestoId)
                .OrderBy(sr => sr.Repuesto.Nombre)
                .ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_StockRepuestosList", listPage);
            }

            ViewBag.proveedorId = new SelectList(db.Proveedores, "ProveedorId", "Nombre");
            ViewBag.repuestoId = new SelectList(db.Repuestos, "RepuestoId", "Nombre");
            return View(listPage);
        }

        // GET: StockRepuestos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockRepuestos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StockRepuestos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StockRepuestos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StockRepuestos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StockRepuestos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StockRepuestos/Delete/5
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
