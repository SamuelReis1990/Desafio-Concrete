using System.Collections.Generic;

namespace DesafioConcrete.API.Models
{
    public class UsuarioViewModel : LoginViewModel
    {
        public UsuarioViewModel()
        {
            Telefones = new List<TelefoneViewModel>();
        }

        public string Nome { get; set; }        
        public ICollection<TelefoneViewModel> Telefones { get; set; }
    }
}