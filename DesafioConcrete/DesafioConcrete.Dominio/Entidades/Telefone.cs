using System;

namespace DesafioConcrete.Dominio.Entidades
{
    public class Telefone
    {
        public Telefone()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public int Numero { get; set; }
        public int DDD { get; set; }
        
        public Usuario Usuario { get; set; }
    }
}