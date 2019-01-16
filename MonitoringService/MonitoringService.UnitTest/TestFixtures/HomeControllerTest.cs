using MonitoringService.Controllers;
using MonitoringService.Interfaces;
using MonitoringService.Models;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;

namespace MonitoringService.UnitTest.TestFixtures
{
    /// <summary>
    /// Test class for HomeController
    /// </summary>
    [TestFixture]
    public class HomeControllerTest : ServiceTestBase
    {
        #region Fields

        private readonly Mock<IRepository<Service>> _mockServiceRepository;
        private readonly HomeController _homeController;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public HomeControllerTest()
        {
            _mockServiceRepository = new Mock<IRepository<Service>>();
            _homeController = new HomeController(_mockServiceRepository.Object);
        }

        #endregion

        #region Tests

        /// <summary>
        /// Tests the GetService action
        /// </summary>
        [Test]
        [Explicit ("File access is not handled")]
        public void Index_Returns_Html()
        {
            var result = (VirtualFileResult)_homeController.Index();
            Assert.AreEqual("index.html", result.FileName);
            Assert.AreEqual("text/html", result.ContentType);
        }

        /// <summary>
        /// Tests the GetService action
        /// </summary>
        [Test]
        [Explicit("HttpContext access is not handled")]
        public void JsonService_Returns_GetService()
        {
            _mockServiceRepository
                .Setup(repo => repo.GetOneItem())
                .Returns(ActiveService);

            var result = (JsonResult)_homeController.GetService();
            Assert.AreEqual(ActiveService, (Service)result.Value);
        }

        #endregion
    }
}
