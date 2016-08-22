using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquiposTecnicosSN.Web;
using EquiposTecnicosSN.Web.Controllers;

namespace EquiposTecnicosSN.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index(new Models.BuscarEquipoViewModel()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
