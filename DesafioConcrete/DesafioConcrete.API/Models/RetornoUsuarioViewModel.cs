using System;

namespace DesafioConcrete.API.Models
{
    public class RetornoUsuarioViewModel : UsuarioViewModel
    {
        public string Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime UltimoLogin { get; set; }
        public string Token { get; set; }
    }
}