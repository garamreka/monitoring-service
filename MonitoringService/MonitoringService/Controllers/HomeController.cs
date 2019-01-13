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
        /// Provides the result in json format
        /// </summary>
        /// <returns>With result</returns>
        [HttpGet]
        [Route("/api")]
        public IActionResult Index()
        {
            try
            {
                return Json(_serviceRepository.GetItemById(1)); //Currently there is only one item in source file
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
            
        }

        #endregion
    }
}
