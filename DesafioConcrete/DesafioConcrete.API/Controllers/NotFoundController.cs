using DesafioConcrete.API.Enums;
using DesafioConcrete.API.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;

namespace DesafioConcrete.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions, AcceptVerbs("PATCH")]
        public RetornoMensagem Erro404()
        {
            RetornoMensagem retorno = null;

            retorno = new RetornoMensagem
            {
                StatusCode = (int)StatusCodeEnum.NotFound,
                Mensagem = StatusCodeEnum.NotFound.ToString()
            };

            return retorno;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpNotFoundAwareDefaultHttpControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public HttpNotFoundAwareDefaultHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectController(request);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound)
                    throw;
                var routeValues = request.GetRouteData().Values;
                routeValues["controller"] = "NotFound";
                routeValues["action"] = "Erro404";
                decriptor = base.SelectController(request);
            }
            return decriptor;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpNotFoundAwareControllerActionSelector : ApiControllerActionSelector
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpNotFoundAwareControllerActionSelector()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            HttpActionDescriptor decriptor = null;
            try
            {
                decriptor = base.SelectAction(controllerContext);
            }
            catch (HttpResponseException ex)
            {
                var code = ex.Response.StatusCode;
                if (code != HttpStatusCode.NotFound && code != HttpStatusCode.MethodNotAllowed)
                    throw;
                var routeData = controllerContext.RouteData;
                routeData.Values["action"] = "Erro404";
                IHttpController httpController = new NotFoundController();
                controllerContext.Controller = httpController;
                controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());
                decriptor = base.SelectAction(controllerContext);
            }
            return decriptor;
        }
    }
}