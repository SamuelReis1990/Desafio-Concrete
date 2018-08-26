using System.Collections.Generic;

namespace DesafioConcrete.API.Models
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Telefones = new List<TelefoneViewModel>();
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ICollection<TelefoneViewModel> Telefones { get; set; }
    }
}