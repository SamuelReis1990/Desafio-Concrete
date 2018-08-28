using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace DesafioConcrete.API.Jwt
{
    public class JwtAutorizacaoAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;        

        public async Task AuthenticateAsync(HttpAuthenticationContext contexto, CancellationToken cancelamentoToken)
        {            
            var autorizacao = contexto.Request.Headers.Authorization;

            if (autorizacao == null || autorizacao.Scheme != "Bearer")
            {
                System.Web.HttpContext.Current.Session["Token"] = "";
                return;
            }

            System.Web.HttpContext.Current.Session["Token"] = autorizacao.Parameter;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext contexto, CancellationToken cancelamentoToken)
        {
            return Task.FromResult(0);
        }
    }
}
