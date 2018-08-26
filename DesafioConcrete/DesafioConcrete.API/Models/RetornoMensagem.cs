using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioConcrete.API.Models
{
    public class RetornoMensagem
    {
        public RetornoMensagem()
        {
            StatusCode = "200";
            Mensagem = "OK";
        }

        public string StatusCode { get; set; }
        public string Mensagem { get; set; }
    }
}