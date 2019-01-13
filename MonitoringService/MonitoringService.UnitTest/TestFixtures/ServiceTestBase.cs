using System;
using System.Collections.Generic;
using MonitoringService.Enums;
using MonitoringService.Models;

namespace MonitoringService.UnitTest.TestFixtures
{
    /// <summary>
    /// Implementation of ServiceTestBase class
    /// </summary>
    public abstract class ServiceTestBase
    {
        #region Fields

        protected readonly string PassiveServiceLineInFile = "P0551234567";
        protected readonly string ActiveServiceLineInFile = "A0551234567          EE 201110231600";
        protected readonly string ActiveXlServiceLineInFile = "A0551234567          JE 20111023160008001200E";
        protected readonly string OverrideListInUseLineInFile 
            = "A0551234567          JE 20111023160008001200K05512345670551234567Peeter              Timo Tamm           ";

        protected readonly string[] TestSourceFile = new[]
        {
            "A0551234567          JE 20111023160008001200E"
        };

        protected readonly Service PassiveService = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234567",
            IsActive = false
        };

        protected readonly Service ActiveService = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234567",
            IsActive = true,
            ServiceLanguage = Language.Estonian,
            ExpiryDateAndTime = new DateTime(2011, 10, 23, 16, 00, 00),
            IsXlServiceActive = false,
        };

        protected readonly Service ActiveXlService = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234567",
            IsActive = true,
            ServiceLanguage = Language.Estonian,
            ExpiryDateAndTime = new DateTime(2011, 10, 23, 16, 00, 00),
            IsXlServiceActive = true,
            XlService = new XlService()
            {
                XlServiceLanguage = Language.Undefined,
                XlServiceActivationTime = new TimeSpan(08, 00, 00),
                XlServiceEndTime = new TimeSpan(12, 00, 00),
                IsOverrideListInUse = false
            }
        };

        protected readonly Service ServiceWithOverrideList = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234567",
            IsActive = true,
            ServiceLanguage = Language.Estonian,
            ExpiryDateAndTime = new DateTime(2011, 10, 23, 16, 00, 00),
            IsXlServiceActive = true,
            XlService = new XlService()
            {
                XlServiceLanguage = Language.Undefined,
                XlServiceActivationTime = new TimeSpan(08, 00, 00),
                XlServiceEndTime = new TimeSpan(12, 00, 00),
                IsOverrideListInUse = false,
                Contacts = new List<Contact>()
                {
                    new Contact()
                    {
                        PhoneNumber = "0551234567",
                        Name = "Peeter"
                    },
                    new Contact()
                    {
                        PhoneNumber = "0551234567",
                        Name = "Timo Tamm"
                    }
                }
            }
        };

        #endregion
    }
}
