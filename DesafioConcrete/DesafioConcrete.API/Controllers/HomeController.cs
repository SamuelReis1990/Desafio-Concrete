using System.Web.Mvc;

namespace DesafioConcrete.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home API Desafio Concrete";

            return View();
        }       
    }
}
