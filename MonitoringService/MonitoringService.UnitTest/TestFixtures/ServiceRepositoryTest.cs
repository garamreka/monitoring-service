using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MonitoringService.Interfaces;
using MonitoringService.Models;
using MonitoringService.Repositories;
using Moq;

namespace MonitoringService.UnitTest.TestFixtures
{
    /// <summary>
    /// Test class for ServiceRepository
    /// </summary>
    [TestFixture]
    public class ServiceRepositoryTest : ServiceTestBase
    {
        #region Fields

        private readonly Mock<IParser> _mockServiceParameterParser;
        private readonly ServiceRepository _serviceRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceRepositoryTest()
        {
            _mockServiceParameterParser = new Mock<IParser>();
            _serviceRepository = new ServiceRepository(_mockServiceParameterParser.Object);
        }

        #endregion

        #region Tests

        /// <summary>
        /// Tests the GetAllItem method with valid input
        /// </summary>
        [Test]
        public void GetAllItem_Returns_ValidServices()
        {
            _mockServiceParameterParser
                .Setup(parser => parser.ParseToService(_activeXlServiceFile))
                .Returns(_activeXlService);

            var expectedResult = new List<Service>()
            {
                _activeXlService
            };

            var result = _serviceRepository.GetAllItem();

            Assert.AreEqual(_activeXlServiceFile, result);
        }

        /// <summary>
        /// Tests the GetItemById with valid input
        /// </summary>
        [Test]
        public void GetItemById_Returns_ValidService()
        {
            _mockServiceParameterParser
                .Setup(parser => parser.ParseToService(_activeXlServiceFile))
                .Returns(_activeXlService);

            var result = _serviceRepository.GetItemById(1);

            Assert.AreEqual(_activeXlServiceFile, result);
        }

        /// <summary>
        /// Tests the GetItemById with valid input
        /// </summary>
        [Test]
        public void GetItemById_InvalidId_ThrowsException()
        {
            //_mockServiceParameterParser
            //    .Setup(parser => parser.ParseToService(_activeXlServiceFile))
            //    .Returns(_activeXlService);

            Assert.Throws<ArgumentOutOfRangeException>(() => _serviceRepository.GetItemById(0));
        }

        /// <summary>
        /// Tests the GetItemById with valid input
        /// </summary>
        [Test]
        public void GetItemById_OutOfRangeId_ThrowsException()
        {
            //_mockServiceParameterParser
            //    .Setup(parser => parser.ParseToService(_activeXlServiceFile))
            //    .Returns(_activeXlService);

            Assert.Throws<NullReferenceException>(() => _serviceRepository.GetItemById(10));
        }

        #endregion
    }
}
