using DesafioConcrete.Dominio.Entidades;
using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Ado.Net;
using DesafioConcrete.Infra.Contextos;
using System.Linq;

namespace DesafioConcrete.Infra.Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {

        public Usuario RecuperarUsuario(string email)
        {
            using (var db = new EFContexto())
            {
                return db.Usuario.Where(u => u.Email.Equals(email)).FirstOrDefault();
            }
        }

        public Usuario RecuperarUsuarioAdoNet(string id)
        {
            return ConexaoAdoNet.RecuperaUsuario(id);
        }

        public bool VerificaExisteEmailCadastrado(string email)
        {
            using (var db = new EFContexto())
            {
                return db.Usuario.Count(u => u.Email.Equals(email)) > 0;
            }
        }

        public bool VerificaSenhaCadastrada(string email, string senha)
        {
            using (var db = new EFContexto())
            {
                return db.Usuario.Count(u => u.Email.Equals(email) && u.Senha.Equals(senha)) > 0;
            }
        }
    }
}