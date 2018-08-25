using System.Linq;

namespace DesafioConcrete.Dominio.Interfaces
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class
    {
        IQueryable<TEntidade> GetAll();
    }
}
