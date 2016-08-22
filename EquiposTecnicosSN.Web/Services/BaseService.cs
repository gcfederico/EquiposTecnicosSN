using EquiposTecnicosSN.Web.DataContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiposTecnicosSN.Web.Services
{
    public class BaseService
    {
        protected EquiposDbContext equiposDb = new EquiposDbContext();
        protected IdentityDb identityDb = new IdentityDb();
    }
}