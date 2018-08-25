using DesafioConcrete.Dominio.Interfaces;
using System.Linq;
using DesafioConcrete.Infra.Contextos;

namespace DesafioConcrete.Infra.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class
    {
        EFContexto db = null;

        public RepositorioBase()
        {
            db = new EFContexto();
        }

        public IQueryable<TEntidade> GetAll()
        {            
            return db.Set<TEntidade>();
        }
    }
}
