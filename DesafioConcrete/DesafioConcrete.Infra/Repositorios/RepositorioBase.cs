using DesafioConcrete.Dominio.Entidades;
using DesafioConcrete.Dominio.Interfaces;
using DesafioConcrete.Infra.Contextos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public Usuario RecuperarUsuario(string email)
        {
            using (var db = new EFContexto())
            {
                return db.Usuario.Where(u => u.Email.Equals(email)).FirstOrDefault();
            }
        }

        public IEnumerable<Telefone> RecuperarTelefones(string id)
        {
            using (var db = new EFContexto())
            {
                return db.Telefones.Where(u => u.UsuarioId.Equals(id)).ToList();
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

        public bool VerificaExisteEmailCadastrado(string email)
        {
            using (var db = new EFContexto())
            {
                return db.Usuario.Count(u => u.Email.Equals(email)) > 0;
            }
        }

        public bool VerificaSenhaCadastrada(string email, string senha)
        {
            using (var db = new EFContexto())
            {
                 return db.Usuario.Count(u => u.Email.Equals(email) && u.Senha.Equals(senha)) > 0;
            }
        }
    }
}
