using System;
using System.Collections.Generic;

namespace DesafioConcrete.Dominio.Entidades
{
    public class Usuario
    {
        public Usuario()
        {            
            Id = Guid.NewGuid().ToString();
            Token = Guid.NewGuid().ToString();
            DataCriacao = DateTime.Now;
            UltimoLogin = DateTime.Now;
            Telefone = new List<Telefone>();
        }

        public string Id { get; set; }        
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }                
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime UltimoLogin { get; set; }
        public string Token { get; set; }

        public ICollection<Telefone> Telefone { get; set; }
    }
}
