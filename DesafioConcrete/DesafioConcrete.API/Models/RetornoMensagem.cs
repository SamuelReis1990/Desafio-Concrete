namespace DesafioConcrete.API.Models
{
    public class RetornoMensagem
    {
        public RetornoMensagem()
        {
            StatusCode = 200;
            Mensagem = "OK";
        }

        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}