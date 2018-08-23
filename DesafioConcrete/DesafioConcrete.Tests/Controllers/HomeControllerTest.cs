using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioConcrete.API;
using DesafioConcrete.API.Controllers;

namespace DesafioConcrete.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Organizar
            HomeController controller = new HomeController();

            // Agir
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home API Desafio Concrete", result.ViewBag.Title);
        }
    }
}
