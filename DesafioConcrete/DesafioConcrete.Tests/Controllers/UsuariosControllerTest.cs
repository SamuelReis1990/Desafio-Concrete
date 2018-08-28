using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesafioConcrete.Tests.Controllers
{
    [TestClass]
    public class UsuariosControllerTest
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public UsuariosControllerTest()
        {
            _repositorioUsuario = new RepositorioUsuario();
        }

        [TestMethod]
        public void Test_getUsuario()
        {
            // Organizar                          

            // Agir
            var result = _repositorioUsuario.VerificaExisteEmailCadastrado("");

            // Declarar
            Assert.IsNotNull(result);
            //Assert.IsFalse(result, "");
            //Assert.AreEqual(8, result);
        }
    }
}
