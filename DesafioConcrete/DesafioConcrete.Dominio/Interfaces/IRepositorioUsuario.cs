using DesafioConcrete.Dominio.Entidades;

namespace DesafioConcrete.Dominio.Interfaces
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Usuario RecuperarUsuario(string email);
        Usuario RecuperarUsuarioAdoNet(string id);        
        bool VerificaExisteEmailCadastrado(string email);
        bool VerificaSenhaCadastrada(string email, string senha);
    }
}