using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Contextos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioConcrete.Infra.Repositorios
{
    public class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : class
    {
        public IEnumerable<TEntidade> GetAll()
        {
            using (var db = new EFContexto())
            {
                return db.Set<TEntidade>();
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

        public bool VerificaExisteEmailCadastrado(string email)
        {
            using (var db = new EFContexto())
            {
                return db.Usuario.Count(c => c.Email == email) > 0;
            }
        }
    }
}
