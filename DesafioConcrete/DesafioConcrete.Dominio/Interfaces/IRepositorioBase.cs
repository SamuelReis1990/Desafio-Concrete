using DesafioConcrete.Dominio.Entidades;
using System.Collections.Generic;

namespace DesafioConcrete.Dominio.Interfaces
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class
    {
        TEntidade RecuperarRegistro(string id);
        Usuario RecuperarUsuario(string email);
        IEnumerable<Telefone> RecuperarTelefones(string id);
        string Cadastrar(TEntidade classe);
        string Atualizar(TEntidade classe);
        bool VerificaExisteEmailCadastrado(string email);
        bool VerificaSenhaCadastrada(string email, string senha);
    }
}
