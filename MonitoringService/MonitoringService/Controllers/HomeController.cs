using System;
using Microsoft.AspNetCore.Mvc;
using MonitoringService.Interfaces;
using MonitoringService.Models;

namespace MonitoringService.Controllers
{
    /// <summary>
    /// Implementation of HomeController
    /// </summary>
    public class HomeController : Controller
    {
        #region Fields

        private readonly IRepository<Service> _serviceRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceRepository"></param>
        public HomeController(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Gets index.html page
        /// </summary>
        /// <returns>With index.html</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            try
            {
                return File("index.html", "text/html");
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
            
        }

        /// <summary>
        /// Provides the service result in json format
        /// </summary>
        /// <returns>With result</returns>
        [HttpGet]
        [Route("/api")]
        public IActionResult GetService()
        {
            try
            {
                HttpContext.Response.Headers.Add("refresh", "5; url=" + Url.Action("Index"));
                //Currently there is only one item in source file
                return Json(_serviceRepository.GetOneItem());
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }

        #endregion
    }
}
