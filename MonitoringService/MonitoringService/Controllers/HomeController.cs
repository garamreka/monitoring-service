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

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return File("index.html", "text/html");
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
                //Currently there is only one item in source file
                var service = _serviceRepository.GetOneItem();
                return Json(_serviceRepository.GetOneItem());
                //return Json(new
                //{
                //    requestSequenceId = service.RequestSequenceId,
                //    //phoneNumber = service.PhoneNumber,
                //    //isActive = service.IsActive
                //});
            }
            catch (Exception exception)
            {
                return Json(exception.Message);
            }
        }

        #endregion
    }
}
