using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Controllers
{
    public abstract class ODTController : Controller
    {
        protected EquiposDbContext db = new EquiposDbContext();

        // GET: */Details/id
        abstract public ActionResult Details(int? id);
        
        // GET: */CreateForEquipo/id
        abstract public ActionResult CreateForEquipo(int id);

        abstract public ActionResult CountOrdenesPrioridad(String prioridad);

        abstract public ActionResult EditGastos(int id);

        abstract public ActionResult Close(int id);

        // GET: */AddGasto
        public ActionResult AddGasto()
        {
            return PartialView("_AddGastoToOrden", new GastoOrdenDeTrabajo());
        }

        public void SaveGastos(IEnumerable<GastoOrdenDeTrabajo> gastos, int ordenDeTrabajoId)
        {
            var gastosEntidad = db.GastosOrdenesDeTrabajo.Where(g => g.OrdenDeTrabajoId == ordenDeTrabajoId).ToList();

            foreach (var gastoE in gastosEntidad)
            {
                if (gastos.Any(g => g.GastoOrdenDeTrabajoId == gastoE.GastoOrdenDeTrabajoId))
                {
                    var edicion = gastos.Where(g => g.GastoOrdenDeTrabajoId == gastoE.GastoOrdenDeTrabajoId).Single();
                    gastoE.Monto = edicion.Monto;
                    gastoE.Concepto = edicion.Concepto;
                    db.Entry(gastoE).State = EntityState.Modified;
                }
                else
                {
                    db.GastosOrdenesDeTrabajo.Remove(gastoE);
                }
            }

            var nuevosGastos = gastos.Where(g => g.GastoOrdenDeTrabajoId == 0);
            if (nuevosGastos.Count() > 0)
            {
                nuevosGastos.ToList().ForEach(g => {
                    g.OrdenDeTrabajoId = ordenDeTrabajoId;
                    db.GastosOrdenesDeTrabajo.Add(g);
                });
            }
        }

        public void CloseSolicitudesRepuestos(OrdenDeTrabajo orden)
        {
            foreach (var s in orden.SolicitudesRespuestos)
            {
                if (s.FechaCierre == null)
                {
                    s.FechaCierre = DateTime.Now;
                    db.Entry(s).State = EntityState.Modified;
                }
            }
        }
    }
}
