using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace DesafioConcrete.JwT
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {       
        public bool AllowMultiple => false;
        public string Token { get; set; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;
            var token = authorization.Parameter;
            Token = token;
            //System.Web.HttpContext.Current.Session["Token"] = Token;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {            
            return Task.FromResult(0);
        }
    }
}
