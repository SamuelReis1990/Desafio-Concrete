using DesafioConcrete.API.Enums;
using DesafioConcrete.API.Models;
using DesafioConcrete.Dominio.Entidades;
using DesafioConcrete.Dominio.Interfaces;
using System;
using System.Linq;
using System.Web.Http;
using DesafioConcrete.API.Utils;
using DesafioConcrete.API.Jwt;

namespace DesafioConcrete.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>      
    [RoutePrefix("api/usuario")]
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

        private static RetornoViewModel Retorno(RetornoUsuarioViewModel usuarioRetorno, int statusCode = 200, string mensagem = "OK")
        {
            return new RetornoViewModel
            {
                StatusCode = statusCode.ToString(),
                Mensagem = mensagem,
                Usuario = usuarioRetorno
            };
        }

        #region SingUp

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
        public RetornoViewModel SingUp([FromBody] UsuarioViewModel model)
        {
            try
            {
                model = model ?? new UsuarioViewModel();

                if (String.IsNullOrEmpty(model.Nome) && String.IsNullOrEmpty(model.Email) && String.IsNullOrEmpty(model.Senha))
                {
                    return Retorno(null, (int)StatusCodeEnum.BadRequest, StatusCodeEnum.BadRequest.ToString());
                }

                if (_repositorioUsuario.VerificaExisteEmailCadastrado(model.Email))
                {
                    return Retorno(null, (int)StatusCodeEnum.Conflict, StatusCodeEnum.Conflict.ToString() + ": E-mail já existente!");
                }
                else
                {
                    var usuarioModel = new Usuario { Nome = model.Nome, Email = model.Email, Senha = Utilitarios.CriptografarMD5(model.Senha), Token = JwToken.GerarToken(model.Email) };

                    if (model.Telefones.Any())
                    {
                        foreach (var telefone in model.Telefones)
                        {
                            if (!String.IsNullOrEmpty(telefone.DDD) && !String.IsNullOrEmpty(telefone.Numero))
                            {
                                usuarioModel.Telefones.Add(new Telefone()
                                {
                                    DDD = int.Parse(telefone.DDD),
                                    Numero = int.Parse(telefone.Numero),
                                    UsuarioId = usuarioModel.Id
                                });
                            }
                        }
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

                        return Retorno(retorno, (int)StatusCodeEnum.Created, StatusCodeEnum.Created.ToString() + ": Usuário cadastrado com sucesso!");
                    }
                    else
                    {
                        return Retorno(null, (int)StatusCodeEnum.BadRequest, StatusCodeEnum.BadRequest.ToString() + ": " + retornoCommit);
                    }
                }
            }
            catch (Exception e)
            {
                return Retorno(null, (int)StatusCodeEnum.InternalServerError, StatusCodeEnum.InternalServerError.ToString() + ": " + e.Message);
            }
        }

        #endregion

        #region Login

        /// <summary>
        /// Login Usuário
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>Método para login de usuário</remarks>
        /// <response code="200">OK</response>              
        /// <response code="400">Bad Request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Not Found</response>        
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>        
        [HttpPut, Route("login")]
        public RetornoViewModel Login([FromBody] LoginViewModel model)
        {
            try
            {
                model = model ?? new LoginViewModel();

                if (String.IsNullOrEmpty(model.Email) && String.IsNullOrEmpty(model.Senha))
                {
                    return Retorno(null, (int)StatusCodeEnum.BadRequest, StatusCodeEnum.BadRequest.ToString());
                }

                if (!_repositorioUsuario.VerificaExisteEmailCadastrado(model.Email))
                {
                    return Retorno(null, (int)StatusCodeEnum.NotFound, StatusCodeEnum.NotFound.ToString() + ": Usuário e/ou senha inválidos!");
                }
                else
                {
                    if (!_repositorioUsuario.VerificaSenhaCadastrada(model.Email, Utilitarios.CriptografarMD5(model.Senha)))
                    {
                        return Retorno(null, (int)StatusCodeEnum.Unauthorized, StatusCodeEnum.Unauthorized.ToString() + ": Usuário e/ou senha inválidos!");
                    }
                    else
                    {
                        var usuarioRetorno = _repositorioUsuario.RecuperarUsuario(model.Email);
                        var telefonesRetorno = _repositorioTelefone.RecuperarTelefones(usuarioRetorno.Id);

                        usuarioRetorno.UltimoLogin = DateTime.Now;
                        usuarioRetorno.DataAtualizacao = DateTime.Now;

                        var retornoCommit = _repositorioUsuario.Atualizar(usuarioRetorno);

                        if (String.IsNullOrEmpty(retornoCommit))
                        {
                            RetornoUsuarioViewModel retorno = new RetornoUsuarioViewModel()
                            {
                                Nome = usuarioRetorno.Nome,
                                Email = usuarioRetorno.Email,
                                Senha = usuarioRetorno.Senha,
                                Telefones = telefonesRetorno.Select(t => new TelefoneViewModel
                                {
                                    DDD = t.DDD.ToString(),
                                    Numero = t.Numero.ToString()
                                }).ToList(),
                                Id = usuarioRetorno.Id,
                                DataCriacao = usuarioRetorno.DataCriacao,
                                DataAtualizacao = usuarioRetorno.DataAtualizacao,
                                UltimoLogin = usuarioRetorno.UltimoLogin,
                                Token = usuarioRetorno.Token
                            };

                            return Retorno(retorno, (int)StatusCodeEnum.OK, StatusCodeEnum.OK.ToString() + ": Usuário logado com sucesso!");
                        }
                        else
                        {
                            return Retorno(null, (int)StatusCodeEnum.BadRequest, StatusCodeEnum.BadRequest.ToString() + ": " + retornoCommit);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return Retorno(null, (int)StatusCodeEnum.InternalServerError, StatusCodeEnum.InternalServerError.ToString() + ": " + e.Message);
            }
        }

        #endregion

        #region Profile

        /// <summary>
        /// Perfil Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>Método perfil do usuário</remarks>
        /// <response code="200">OK</response>                     
        /// <response code="401">Unauthorized</response>    
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        /// <returns></returns>    
        [JwtAutorizacao]
        [HttpGet, Route("profile")]
        public RetornoViewModel Profile(string id)
        {
            try
            {
                var token = System.Web.HttpContext.Current.Session["Token"].ToString();

                if (String.IsNullOrEmpty(token))
                {
                    return Retorno(null, (int)StatusCodeEnum.Unauthorized, StatusCodeEnum.Unauthorized.ToString() + ": Não autorizado!");
                }
                else
                {
                    var usuarioRetorno = _repositorioUsuario.RecuperarRegistro(id);
                    var telefonesRetorno = _repositorioTelefone.RecuperarTelefones(id);

                    if (usuarioRetorno == null)
                    {
                        return Retorno(null, (int)StatusCodeEnum.NotFound, StatusCodeEnum.NotFound.ToString() + ": Usuário não encontrado!");
                    }

                    if (usuarioRetorno.Token.Equals(token))
                    {
                        TimeSpan tempo = DateTime.Now - usuarioRetorno.UltimoLogin;

                        if (tempo.Minutes > 30)
                        {
                            return Retorno(null, (int)StatusCodeEnum.Unauthorized, StatusCodeEnum.Unauthorized.ToString() + ": Sessão inválida!");
                        }
                    }
                    else
                    {
                        return Retorno(null, (int)StatusCodeEnum.Forbidden, StatusCodeEnum.Forbidden.ToString() + ": Não autorizado!");
                    }

                    RetornoUsuarioViewModel retorno = new RetornoUsuarioViewModel()
                    {
                        Nome = usuarioRetorno.Nome,
                        Email = usuarioRetorno.Email,
                        Senha = usuarioRetorno.Senha,
                        Telefones = telefonesRetorno.Select(t => new TelefoneViewModel
                        {
                            DDD = t.DDD.ToString(),
                            Numero = t.Numero.ToString()
                        }).ToList(),
                        Id = usuarioRetorno.Id,
                        DataCriacao = usuarioRetorno.DataCriacao,
                        DataAtualizacao = usuarioRetorno.DataAtualizacao,
                        UltimoLogin = usuarioRetorno.UltimoLogin,
                        Token = usuarioRetorno.Token
                    };

                    return Retorno(retorno, (int)StatusCodeEnum.OK, StatusCodeEnum.OK.ToString());
                }
            }
            catch (Exception e)
            {
                return Retorno(null, (int)StatusCodeEnum.InternalServerError, StatusCodeEnum.InternalServerError.ToString() + ": " + e.Message);
            }
        }

        #endregion
    }
}