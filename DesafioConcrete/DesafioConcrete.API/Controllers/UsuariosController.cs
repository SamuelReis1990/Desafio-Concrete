using DesafioConcrete.Dominio.Interfaces;
using System.Linq;
using System.Web.Http;

namespace DesafioConcrete.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("Api")]
    public class UsuariosController : ApiController
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioTelefone _repositorioTelefone;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositorioUsuario"></param>
        /// <param name="repositorioTelefone"></param>
        public UsuariosController(IRepositorioUsuario repositorioUsuario, IRepositorioTelefone repositorioTelefone)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioTelefone = repositorioTelefone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public IQueryable getUsuario()
        {
            return _repositorioUsuario.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("telefone")]
        public IQueryable getTelefone()
        {
            return _repositorioTelefone.GetAll();
        }
    }
}