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
    public class EquiposBaseController : Controller
    {
        protected EquiposDbContext db = new EquiposDbContext();

        // GET: EquiposBase
        public virtual ActionResult Index()
        {
            ViewBag.UbicacionId = new SelectList(db.Ubicaciones.OrderBy(u => u.Nombre), "UbicacionId", "Nombre");
            ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre");
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
                ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre");
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
                ViewBag.SectorId = new SelectList(db.Sectores.OrderBy(u => u.Nombre), "SectorId", "Nombre", equipo.SectorId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buscarNombreCompleto"></param>
        /// <param name="buscarUMDNS"></param>
        /// <param name="UbicacionId"></param>
        /// <param name="SectorId"></param>
        /// <param name="Estado"></param>
        /// <param name="NumeroMatricula"></param>
        /// <returns></returns>
        public ActionResult SearchEquipos(string buscarNombreCompleto, string buscarUMDNS, int? UbicacionId, int? SectorId, int? EstadoEquipo, int? NumeroMatricula, int? SearchTipoEquipo)
        {

            var result = db.Equipos
                .Where(e => buscarNombreCompleto.Equals("") || e.NombreCompleto.Equals(buscarNombreCompleto))
                .Where(e => buscarUMDNS.Equals("") || e.UMDNS.Equals(buscarUMDNS))
                .Where(e => UbicacionId == null || e.UbicacionId == UbicacionId)
                .Where(e => SectorId == null || e.SectorId == SectorId)
                .Where(e => NumeroMatricula == null || e.NumeroMatricula.Equals(NumeroMatricula))
                .ToList();


            if (EstadoEquipo != 0)
            {
                EstadoDeEquipo estadoFiltro = (EstadoDeEquipo)EstadoEquipo;
                result = result.Where(e => e.Estado.Equals(estadoFiltro)).ToList();
            }

            if (SearchTipoEquipo != 0)
            {
                TipoEquipo tipo = (TipoEquipo)SearchTipoEquipo;

                switch (tipo)
                {
                    case TipoEquipo.Cirugia:
                        result = result.Where(e => e is EquipoCirugia).ToList();
                        break;

                    case TipoEquipo.Climatizacion:
                        result = result.Where(e => e is EquipoClimatizacion).ToList();
                        break;

                    case TipoEquipo.Edilicio:
                        result = result.Where(e => e is EquipamientoEdilicio).ToList();
                        break;

                    case TipoEquipo.Endoscopia:
                        result = result.Where(e => e is EquipoEndoscopia).ToList();
                        break;

                    case TipoEquipo.GasesMedicinales:
                        result = result.Where(e => e is EquipoGasesMedicinales).ToList();
                        break;

                    case TipoEquipo.Imagenes:
                        result = result.Where(e => e is EquipoImagen).ToList();
                        break;

                    case TipoEquipo.Informatica:
                        result = result.Where(e => e is EquipoInformatica).ToList();
                        break;

                    case TipoEquipo.Luces:
                        result = result.Where(e => e is EquipoLuces).ToList();
                        break;

                    case TipoEquipo.Monitoreo:
                        result = result.Where(e => e is EquipoMonitoreo).ToList();
                        break;

                    case TipoEquipo.Odontologia:
                        result = result.Where(e => e is EquipoOdontologia).ToList();
                        break;

                    case TipoEquipo.Otros:
                        result = result.Where(e => e is EquipoOtro).ToList();
                        break;

                    case TipoEquipo.PruebasDeDiagnostico:
                        result = result.Where(e => e is EquipoPruebaDeDiagnostico).ToList();
                        break;

                    case TipoEquipo.Rehabilitacion:
                        result = result.Where(e => e is EquipoRehabilitacion).ToList();
                        break;

                    case TipoEquipo.SoporteDeVida:
                        result = result.Where(e => e is EquipoSoporteDeVida).ToList();
                        break;

                    case TipoEquipo.Terapeutica:
                        result = result.Where(e => e is EquipoTerapeutica).ToList();
                        break;
                }
            }

            return PartialView("_SearchEquiposResults", result.OrderByDescending(e => e.NombreCompleto));
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
