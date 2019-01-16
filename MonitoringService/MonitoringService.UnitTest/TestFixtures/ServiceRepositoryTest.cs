using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
        /// Tests the GetAllItem method
        /// </summary>
        [Test]
        public void GetAllItem_Returns_ValidServices()
        {
            _mockServiceParameterParser
                .Setup(parser => parser.ReadFile())
                .Returns(TestSourceFile);

            _mockServiceParameterParser
                .Setup(parser => parser.ParseToService(ActiveXlServiceLineInFile))
                .Returns(ActiveXlService);

            var expectedResult = new List<Service>()
            {
                ActiveXlService
            };

            var result = _serviceRepository.GetAllItem();

            Assert.AreEqual(expectedResult, result.ToList());
        }

        /// <summary>
        /// Tests the GetOneItem method
        /// </summary>
        [Test]
        public void GetOneItem_Returns_ValidService()
        {
            _mockServiceParameterParser
                .Setup(parser => parser.ReadFile())
                .Returns(TestSourceFile);

            _mockServiceParameterParser
                .Setup(parser => parser.ParseToService(ActiveXlServiceLineInFile))
                .Returns(ActiveXlService);

            var result = _serviceRepository.GetOneItem();

            Assert.AreEqual(ActiveXlService, result);
        }

        #endregion
    }
}
