using Salud.Security.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web.Filters
{
    /// <summary>
    /// Filtro que agrega el nombre del usuario logueado en el ViewBag de cada pagina.
    /// </summary>
    public class CurrentIdentityActionFilter : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (SSOHelper.CurrentIdentity != null)
            {
                filterContext.Controller.ViewBag.CurrentIdentity = SSOHelper.CurrentIdentity.Fullname;
            }
        }

    }
}