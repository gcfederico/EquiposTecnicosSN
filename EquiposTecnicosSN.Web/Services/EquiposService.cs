using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using EquiposTecnicosSN.Entities.Equipos;
using EquiposTecnicosSN.Entities.Mantenimiento;
using EquiposTecnicosSN.Web.DataContexts;


namespace EquiposTecnicosSN.Web.Services
{
    public class EquiposService : BaseService
    {

        public List<EquipoClimatizacion> EquiposClimatizacionDeUbicacion(int ubicacionId)
        {
            var equiposC = db.EquiposDeClimatizacion
                .Include(e => e.InformacionComercial)
                .Include(e => e.Ubicacion)
                .Include(e => e.OrdenesDeTrabajo);

            if (ubicacionId != 0)
            {
                equiposC = equiposC.Where(e => e.UbicacionId == ubicacionId);
            }

            return equiposC.ToList();
        }


        public Equipo BuscarEquipo(int id)
        {
            var equipo = db.Equipos
                .Include(e => e.Traslados)
                .Include(e => e.OrdenesDeTrabajo)
                .Include(e => e.InformacionComercial)
                .Include(e => e.InformacionHardware)
                .Where(e => e.EquipoId == id).Single();

            equipo.OrdenesDeTrabajo = equipo.OrdenesDeTrabajo.OrderByDescending(o => o.FechaInicio).ToList();

            return equipo;
        }

        public List<Equipo> EquiposFuncionales ()
        {
            return db.Equipos
                .Where(e => e.Estado == EstadoDeEquipo.Funcional || e.Estado == EstadoDeEquipo.FuncionalRequiereReparacion)
                .OrderBy(e => e.NombreCompleto)
                .ToList();
        }

        public int EquiposFuncionalesCount()
        {
            return db.Equipos
                .Where(e => e.Estado == EstadoDeEquipo.Funcional || e.Estado == EstadoDeEquipo.FuncionalRequiereReparacion)
                .OrderBy(e => e.NombreCompleto)
                .Count();
        }
    }
}