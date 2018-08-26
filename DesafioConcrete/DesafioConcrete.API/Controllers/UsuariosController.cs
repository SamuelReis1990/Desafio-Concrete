using DesafioConcrete.API.Enums;
using DesafioConcrete.API.Models;
using DesafioConcrete.Dominio.Entidades;
using DesafioConcrete.Dominio.Interfaces;
using System;
using System.Linq;
using System.Web.Http;
using DesafioConcrete.API.Utils;

namespace DesafioConcrete.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("Api")]
    public class UsuariosController : ApiController
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioTelefone _repositorioTelefone;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositorioUsuario"></param>
        /// <param name="repositorioTelefone"></param>
        public UsuariosController(IRepositorioUsuario repositorioUsuario, IRepositorioTelefone repositorioTelefone)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioTelefone = repositorioTelefone;            
        }

        private static RetornoSingUpViewModel RetornoSingUp(RetornoUsuarioViewModel usuarioRetorno, int statusCode = 200, string mensagem = "OK")
        {
            return new RetornoSingUpViewModel
            {
                StatusCode = statusCode.ToString(),
                Mensagem = mensagem,
                Usuario = usuarioRetorno
            };
        }

        /// <summary>
        /// Cadastrar Usuário
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>Método para cadastro de usuário</remarks>
        /// <response code="200">OK</response>
        /// <response code="201">Created</response>        
        /// <response code="400">Bad Request</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>
        [HttpPost, Route("singUp")]
        public RetornoSingUpViewModel SingUp([FromBody] UsuarioViewModel model)
        {
            try
            {
                model = model ?? new UsuarioViewModel();

                if (String.IsNullOrEmpty(model.Nome) && String.IsNullOrEmpty(model.Email) && String.IsNullOrEmpty(model.Senha) && !model.Telefones.Any())
                {
                    return RetornoSingUp(null, (int)StatusCodeEnum.BadRequest, StatusCodeEnum.BadRequest.ToString());
                }

                if (_repositorioUsuario.VerificaExisteEmailCadastrado(model.Email))
                {
                    return RetornoSingUp(null, (int)StatusCodeEnum.Conflict, StatusCodeEnum.Conflict.ToString() + ": E-mail já existente!");
                }
                else
                {
                    var usuarioModel = new Usuario { Nome = model.Nome, Email = model.Email, Senha = Utilitarios.CriptografarMD5(model.Senha) };

                    foreach (var telefone in model.Telefones)
                    {
                        usuarioModel.Telefones.Add(new Telefone()
                        {
                            DDD = int.Parse(telefone.DDD),
                            Numero = int.Parse(telefone.Numero),
                            UsuarioId = usuarioModel.Id                            
                        });
                    }
                    
                    var retornoCommit = _repositorioUsuario.Cadastrar(usuarioModel);

                    if (String.IsNullOrEmpty(retornoCommit))
                    {
                        RetornoUsuarioViewModel retorno = new RetornoUsuarioViewModel()
                        {
                            Nome = usuarioModel.Nome,
                            Email = usuarioModel.Email,
                            Senha = usuarioModel.Senha,
                            Telefones = usuarioModel.Telefones.Select(t => new TelefoneViewModel
                            {
                                DDD = t.DDD.ToString(),
                                Numero = t.Numero.ToString()
                            }).ToList(),
                            Id = usuarioModel.Id,
                            DataCriacao = usuarioModel.DataCriacao,
                            DataAtualizacao = usuarioModel.DataAtualizacao,
                            UltimoLogin = usuarioModel.UltimoLogin,
                            Token = usuarioModel.Token
                        };

                        return RetornoSingUp(retorno, (int)StatusCodeEnum.Created, StatusCodeEnum.Created.ToString() + ": Usuário cadastrado com sucesso!");
                    }
                    else
                    {
                        return RetornoSingUp(null, (int)StatusCodeEnum.BadRequest, StatusCodeEnum.BadRequest.ToString() + ": " + retornoCommit);
                    }
                }
            }
            catch (Exception e)
            {
                return RetornoSingUp(null, (int)StatusCodeEnum.InternalServerError, StatusCodeEnum.InternalServerError.ToString() + ": " + e.Message);
            }
        }       
    }
}