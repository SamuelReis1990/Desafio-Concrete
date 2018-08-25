﻿using DesafioConcrete.API.Controllers;
using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
            IQueryable result = _repositorioUsuario.GetAll();

            // Declarar
            Assert.IsNotNull(result);
            //Assert.IsFalse(result, "");
            //Assert.AreEqual(8, result);
        }
    }
}