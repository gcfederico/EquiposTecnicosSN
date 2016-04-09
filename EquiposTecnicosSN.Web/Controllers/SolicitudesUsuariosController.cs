using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities;
using EquiposTecnicosSN.Web.DataContexts;
using EquiposTecnicosSN.Web.Models;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class SolicitudesUsuariosController : IdentityController
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: CreateUser
        public async Task<ActionResult> CreateUser(int solicitudId) 
        {

            var solicitud = await db.SolicitudesUsuarios.FindAsync(solicitudId);
            var usuario = new ApplicationUser { UserName = solicitud.Email, Email = solicitud.Email };
            var result = await UserManager.CreateAsync(usuario, "1234Qwer!·$");

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: SolicitudesUsuarios 
        public async Task<ActionResult> Index()
        {
            var solicitudesUsuarios = db.SolicitudesUsuarios.Include(s => s.Ubicacion);
            return View(await solicitudesUsuarios.ToListAsync());
        }

        // GET: SolicitudesUsuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudUsuario solicitudUsuario = await db.SolicitudesUsuarios.FindAsync(id);
            if (solicitudUsuario == null)
            {
                return HttpNotFound();
            }
            return View(solicitudUsuario);
        }

        // GET: SolicitudesUsuarios/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto");
            return View();
        }

        // POST: SolicitudesUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SolicitudUsuarioId,Email,UbicacionId")] SolicitudUsuario solicitudUsuario)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesUsuarios.Add(solicitudUsuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", solicitudUsuario.UbicacionId);
            return View(solicitudUsuario);
        }

        // GET: SolicitudesUsuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudUsuario solicitudUsuario = await db.SolicitudesUsuarios.FindAsync(id);
            if (solicitudUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", solicitudUsuario.UbicacionId);
            return View(solicitudUsuario);
        }

        // POST: SolicitudesUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SolicitudUsuarioId,Email,UbicacionId")] SolicitudUsuario solicitudUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudUsuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones, "UbicacionId", "NombreCompleto", solicitudUsuario.UbicacionId);
            return View(solicitudUsuario);
        }

        // GET: SolicitudesUsuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudUsuario solicitudUsuario = await db.SolicitudesUsuarios.FindAsync(id);
            if (solicitudUsuario == null)
            {
                return HttpNotFound();
            }
            return View(solicitudUsuario);
        }

        // POST: SolicitudesUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SolicitudUsuario solicitudUsuario = await db.SolicitudesUsuarios.FindAsync(id);
            db.SolicitudesUsuarios.Remove(solicitudUsuario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
