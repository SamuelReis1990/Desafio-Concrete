using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace DesafioConcrete.API.Jwt
{
    public static class JwToken
    {       
        public static string GerarToken(string email)
        {
            var chaveSecreta = GerarChaveSecreta();
            var chaveSimetrica = Convert.FromBase64String(chaveSecreta);
            var tokenManipulacao = new JwtSecurityTokenHandler();

            //var now = DateTime.UtcNow;
            var tokenDescritor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Email, email)
                        }),

                //Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveSimetrica), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenManipulacao.CreateToken(tokenDescritor);
            var token = tokenManipulacao.WriteToken(stoken);

            return token;
        }

        private static string GerarChaveSecreta()
        {
            var hmac = new HMACSHA256();
            return Convert.ToBase64String(hmac.Key);
        }
    }
}
