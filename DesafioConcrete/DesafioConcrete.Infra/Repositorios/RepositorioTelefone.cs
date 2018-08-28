using DesafioConcrete.Dominio.Entidades;
using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Ado.Net;
using DesafioConcrete.Infra.Contextos;
using System.Collections.Generic;
using System.Linq;

namespace DesafioConcrete.Infra.Repositorios
{
    public class RepositorioTelefone: RepositorioBase<Telefone>, IRepositorioTelefone
    {
        public IEnumerable<Telefone> RecuperarTelefones(string id)
        {
            using (var db = new EFContexto())
            {
                return db.Telefones.Where(u => u.UsuarioId.Equals(id)).ToList();
            }
        }

        public ICollection<Telefone> RecuperaTelefonesAdoNet(string id)
        {
            return ConexaoAdoNet.RecuperaTelefones(id) ?? new List<Telefone>();
        }       
    }
}
