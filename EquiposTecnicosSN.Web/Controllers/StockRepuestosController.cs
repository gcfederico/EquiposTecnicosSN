using EquiposTecnicosSN.Web.DataContexts;
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
        public ActionResult Index()
        {
            var model = db.StockRepuestos.ToList();
            return View(model);
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
