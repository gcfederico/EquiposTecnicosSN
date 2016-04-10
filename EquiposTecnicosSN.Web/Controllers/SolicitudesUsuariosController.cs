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
using Microsoft.AspNet.Identity;

namespace EquiposTecnicosSN.Web.Controllers
{
    [Authorize]
    public class SolicitudesUsuariosController : IdentityController
    {
        private EquiposDbContext db = new EquiposDbContext();

        // GET: /SolicitudesUsuarios/CreateUser
        public async Task<ActionResult> CreateUser(int id) 
        {

            var solicitud = await db.SolicitudesUsuarios.FindAsync(id);
            var usuario = new ApplicationUser { UserName = solicitud.Email, Email = solicitud.Email };
            var result = await UserManager.CreateAsync(usuario, "1234Qwer!·$");

            if (result.Succeeded)
            {
                // Mandar Email de contraseña
                var code = await UserManager.GeneratePasswordResetTokenAsync(usuario.Id);
                var callbackUrl = Url.Action("CreatePassword", "SolicitudesUsuarios", new { userId = usuario.Id, code = code , email = usuario.Email }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(usuario.Id, "Creación de Contraseña", "Para crear su contraseña siga el siguiente el enlace: <a href=\"" + callbackUrl + "\">Clic aquí</a>");
                
                db.SolicitudesUsuarios.Remove(solicitud);
                await db.SaveChangesAsync();

                ViewBag.Message = "Usuario creado.";
                //return View();
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //
        // GET: /SolicitudesUsuarios/CreatePassword
        [AllowAnonymous]
        public ActionResult CreatePassword(string code)
        {
            if (code == null)
            {
                View("Error");
            }

            var model = new CreatePasswordViewModel { Code = code, Email = Request.Params["email"]};
            return View(model);
        }

        //
        // POST: /SolicitudesUsuarios/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePassword(CreatePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("CreatePasswordConfirmation");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("CreatePasswordConfirmation");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /SolicitudesUsuarios/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult CreatePasswordConfirmation()
        {
            return View();
        }







        // GET: SolicitudesUsuarios/
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
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
