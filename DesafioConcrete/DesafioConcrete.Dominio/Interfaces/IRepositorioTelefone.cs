using DesafioConcrete.Dominio.Entidades;
using System.Collections.Generic;

namespace DesafioConcrete.Dominio.Interfaces
{
    public interface IRepositorioTelefone: IRepositorioBase<Telefone>
    {        
        IEnumerable<Telefone> RecuperarTelefones(string id);
        ICollection<Telefone> RecuperaTelefonesAdoNet(string id);
    }
}
