using System.Collections.Generic;
using System.Linq;

namespace DesafioConcrete.Dominio.Interfaces
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class
    {
        IEnumerable<TEntidade> GetAll();        
        string Cadastrar(TEntidade classe);        
        bool VerificaExisteEmailCadastrado(string email);        
    }
}
