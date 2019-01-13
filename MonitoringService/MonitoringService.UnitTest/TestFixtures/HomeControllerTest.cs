﻿using MonitoringService.Controllers;
using MonitoringService.Interfaces;
using MonitoringService.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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
        /// Tests the Index action
        /// </summary>
        [Test]
        public void Index_Returns_ServiceJson() //todo
        {
            _mockServiceRepository
                .Setup(repo => repo.GetItemById(It.IsAny<int>()))
                .Returns(ActiveService);

            var result = (JsonResult)_homeController.Index();

            Assert.AreEqual(ActiveService, (Service)result.Value);
        }

        #endregion
    }
}
