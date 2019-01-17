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

        protected readonly string PassiveServiceLineInFile = "P0502234569          EE 201201012359        E";
        protected readonly string ActiveServiceLineInFile = "A0551234567          EE 201110231600";
        protected readonly string ActiveXlServiceLineInFile = "A0551234567          JE 20111023160008001200E";
        protected readonly string OverrideListInUseLineInFile 
            = "A0551234555          JII20111111215900001200K" +
              "0552212211     0506669999                                                                                               Rein Ratas                                                                                                                                                      ";

        protected readonly string[] TestSourceFile = new[]
        {
            "A0551234567          JE 20111023160008001200E"
        };

        protected readonly Service PassiveService = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0502234569",
            IsActive = false
        };

        protected readonly Service ActiveService = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234567",
            IsActive = true,
            ServiceLanguage = Language.Estonian,
            ExpiryDateAndTime = "10. 23. 2011 16:00",
            IsXlServiceActive = false,
        };

        protected readonly Service ActiveXlService = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234567",
            IsActive = true,
            ServiceLanguage = Language.Estonian,
            ExpiryDateAndTime = "10. 23. 2011 16:00",
            IsXlServiceActive = true,
            XlService = new XlService()
            {
                XlServiceLanguage = Language.Undefined,
                XlServiceActivationTime = "08:00",
                XlServiceEndTime = "12:00",
                IsOverrideListInUse = false
            }
        };

        protected readonly Service ServiceWithOverrideList = new Service()
        {
            RequestSequenceId = 1,
            PhoneNumber = "0551234555",
            IsActive = true,
            ServiceLanguage = Language.English,
            ExpiryDateAndTime = "11. 11. 2011 21:59",
            IsXlServiceActive = true,
            XlService = new XlService()
            {
                XlServiceLanguage = Language.English,
                XlServiceActivationTime = "00:00",
                XlServiceEndTime = "12:00",
                IsOverrideListInUse = true,
                Contacts = new List<Contact>()
                {
                    new Contact()
                    {
                        PhoneNumber = "0552212211",
                        Name = "Rein Ratas          "
                    },
                    new Contact()
                    {
                        PhoneNumber = "0506669999",
                        Name = "                    "
                    },
                    new Contact()
                    {
                        PhoneNumber = "          ",
                        Name = "                    "
                    },
                    new Contact()
                    {
                        PhoneNumber = "          ",
                        Name = "                    "
                    },
                    new Contact()
                    {
                        PhoneNumber = "          ",
                        Name = "                    "
                    },
                    new Contact()
                    {
                        PhoneNumber = "          ",
                        Name = "                    "
                    },
                    new Contact()
                    {
                        PhoneNumber = "          ",
                        Name = "                    "
                    },
                    new Contact()
                    {
                        PhoneNumber = "          ",
                        Name = "                    "
                    }
                }
            }
        };

        #endregion
    }
}
