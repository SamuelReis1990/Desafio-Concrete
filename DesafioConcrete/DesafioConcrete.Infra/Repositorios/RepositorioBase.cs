using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Contextos;
using System;
using System.Data.Entity;

namespace DesafioConcrete.Infra.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class
    {
        public TEntidade RecuperarRegistro(string id)
        {
            using (var db = new EFContexto())
            {
                return db.Set<TEntidade>().Find(id);
            }
        }        

        public string Cadastrar(TEntidade classe)
        {
            using (var db = new EFContexto())
            {
                db.Set<TEntidade>().Add(classe);

                try
                {
                    db.SaveChanges();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }

        public string Atualizar(TEntidade classe)
        {
            using (var db = new EFContexto())
            {                
                db.Entry(classe).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return "";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }       
    }
}
