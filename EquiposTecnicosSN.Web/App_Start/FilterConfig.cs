﻿using EquiposTecnicosSN.Web.Filters;
using System.Web.Mvc;

namespace EquiposTecnicosSN.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CurrentIdentityActionFilter());
        }
    }
}
