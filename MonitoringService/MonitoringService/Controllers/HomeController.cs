using Microsoft.AspNetCore.Mvc;

namespace MonitoringService.Controllers
{
    /// <summary>
    /// Implementation of HomeController
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Provides the result in json format
        /// </summary>
        /// <returns>With result</returns>
        [HttpGet]
        [Route("/api")]
        public IActionResult Index()
        {
            return Json(new object());
        }
    }
}
