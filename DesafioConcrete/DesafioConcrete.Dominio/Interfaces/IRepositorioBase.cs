namespace DesafioConcrete.Dominio.Interfaces
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class
    {
        TEntidade RecuperarRegistro(string id);                
        string Cadastrar(TEntidade classe);
        string Atualizar(TEntidade classe);        
    }
}
