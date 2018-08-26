using DesafioConcrete.Dominio.Entidades;

namespace DesafioConcrete.API.Models
{
    public class RetornoSingUpViewModel : RetornoMensagem
    {
        public RetornoUsuarioViewModel Usuario { get; set; }
    }
}