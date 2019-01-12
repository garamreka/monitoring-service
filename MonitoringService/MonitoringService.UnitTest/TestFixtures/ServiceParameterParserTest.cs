﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MonitoringService.Helpers;

namespace MonitoringService.UnitTest.TestFixtures
{
    /// <summary>
    /// Test class for ServiceParameterParser class
    /// </summary>
    [TestFixture]
    public class ServiceParameterParserTest : ServiceTestBase
    {
        #region Fields

        private readonly ServiceParameterParser _serviceParameterParser;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceParameterParserTest()
        {
            _serviceParameterParser = new ServiceParameterParser();
        }

        #endregion

        #region TearDown

        /// <summary>
        /// Sets back Id value to one
        /// </summary>
        [TearDown]
        public void TestTearDown()
        {
            _serviceParameterParser.Id = 1;
        } 

        #endregion

        #region Tests

        /// <summary>
        /// Tests the ParseToService method with passive service
        /// </summary>
        [Test]
        public void ParseToService_Returns_PassiveService()
        {
            var service = _serviceParameterParser.ParseToService(_passiveServiceFile);
            Assert.AreEqual(_passiveService, service);
            Assert.AreEqual(2, _serviceParameterParser.Id);
        }

        /// <summary>
        /// Tests the ParseToService method with active service
        /// </summary>
        [Test]
        public void ParseToService_Returns_ActiveService()
        {
            var service = _serviceParameterParser.ParseToService(_activeServiceFile);
            Assert.AreEqual(_activeService, service);
            Assert.AreEqual(2, _serviceParameterParser.Id);
        }

        /// <summary>
        /// Tests the ParseToService method with active XL service
        /// </summary>
        [Test]
        public void ParseToService_Returns_ActiveXlService()
        {
            var service = _serviceParameterParser.ParseToService(_activeXlServiceFile);
            Assert.AreEqual(_activeXlService, service);
            Assert.AreEqual(2, _serviceParameterParser.Id);
        }

        /// <summary>
        /// Tests the ParseToService method with override list
        /// </summary>
        [Test]
        [Explicit]
        public void ParseToService_Returns_ServiceWithOverrideList()
        {
            var service = _serviceParameterParser.ParseToService(_overrideListInUseFile);
            Assert.AreEqual(_serviceWithOverrideList, service);
            Assert.AreEqual(2, _serviceParameterParser.Id);
        }

        /// <summary>
        /// Tests the ParseToService method with invalid phone number input
        /// </summary>
        [Test]
        public void ParseToService_InvalidPhoneNumber_ThrowsException()
        {
            var line = "P055ABC4567";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid state input
        /// </summary>
        [Test]
        [Explicit]
        public void ParseToService_InvalidState_ThrowsException()
        {
            var line = "C055ABC4567";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid service langugage input
        /// </summary>
        [Test]
        public void ParseToService_InvalidServiceLanguage_ThrowsException()
        {
            var line = "A0551234567          JC 201110231600";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid expiry date input
        /// </summary>
        [Test]
        public void ParseToService_InvalidExpiryDate_ThrowsException()
        {
            var line = "A0551234567          JE 20ABC0231600";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid XL service state input
        /// </summary>
        [Test]
        [Explicit]
        public void ParseToService_InvalidXlServiceState_ThrowsException()
        {
            var line = "A0551234567          OE 201110231600";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid XL service language input
        /// </summary>
        [Test]
        public void ParseToService_InvalidXlServiceLanguage_ThrowsException()
        {
            var line = "A0551234567          JEJ20111023160008001200E";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid XL service activation time input
        /// </summary>
        [Test]
        public void ParseToService_InvalidXlActivationTime_ThrowsException()
        {
            var line = "A0551234567          JE 20111023160008AB1200E";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid XL service end time input
        /// </summary>
        [Test]
        public void ParseToService_InvalidXlEndTime_ThrowsException()
        {
            var line = "A0551234567          JE 20111023160008AB12ABE";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ParseToService method with invalid override list usage input
        /// </summary>
        [Test]
        [Explicit]
        public void ParseToService_InvalidOverrideListInUse_ThrowsException()
        {
            var line = "A0551234567          JE 20111023160008AB1200P";
            Assert.Throws<Exception>(() => _serviceParameterParser.ParseToService(line));
        }

        /// <summary>
        /// Tests the ReadFile method
        /// </summary>
        [Test]
        [Explicit]
        public void ReadFile_Returns_ValidArray()
        {
            
        }

        #endregion
    }
}