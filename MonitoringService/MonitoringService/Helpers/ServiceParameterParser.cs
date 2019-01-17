using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using MonitoringService.Enums;
using MonitoringService.Interfaces;
using MonitoringService.Models;

namespace MonitoringService.Helpers
{
    /// <summary>
    /// Implementation of ServiceParameterParser class
    /// </summary>
    public class ServiceParameterParser : IParser
    {
        #region Fields

        private readonly string _dataSourceBaseUrl = @"https://people.proekspert.ee/ak/";
        private readonly string _activeService = "A";
        private readonly string _notActiveService = "P";
        private readonly string _activeXlService = "J";
        private readonly string _notActiveXlService = "E";
        private readonly string _activeOverrideList = "K";
        private readonly string _notActiveOverrideList = "E";

        /// <summary>
        /// Gives value to RequestSequenceId and index to data source path
        /// </summary>
        public int Index = 1;

        #endregion

        #region Public methods

        /// <summary>
        /// Parses the file line content to Service
        /// </summary>
        /// <returns>With the Service</returns>
        public Service ParseToService(string line)
        {
            if (line == null)
            {
                throw new Exception("Invalid parameter.");
            }
            var service = new Service()
            {
                RequestSequenceId = Index,
                PhoneNumber = ParsePhoneNumber(line.Substring(1, 10)),
                IsActive = ParseIsActive(line.Substring(0, 1), _activeService, _notActiveService)
            };

            if (service.IsActive)
            {
                service.ServiceLanguage = ParseLanguage(line.Substring(22, 1));
                service.ExpiryDateAndTime = ParseDateAndTime(line.Substring(24, 12));
                service.IsXlServiceActive = ParseIsActive(line.Substring(21, 1), _activeXlService, _notActiveXlService);

                if (service.IsXlServiceActive)
                {
                    service.XlService = new XlService()
                    {
                        XlServiceLanguage = ParseLanguage(line.Substring(23, 1)),
                        XlServiceActivationTime = ParseTimeSpan(line.Substring(36, 4)),
                        XlServiceEndTime = ParseTimeSpan(line.Substring(40, 4)),
                        IsOverrideListInUse = ParseIsActive(line.Substring(44, 1), _activeOverrideList, _notActiveOverrideList)
                    };

                    if (service.XlService.IsOverrideListInUse)
                    {
                        service.XlService.Contacts = ParseContactList(line.Substring(45));
                    }
                }
            }

            if (Index == 9)
            {
                Index = 1;
            }
            else
            {
                Index++;
            }
            
            return service;
        }

        /// <summary>
        /// Reads all line of a text file
        /// </summary>
        /// <returns>With the lines of the file</returns>
        public string[] ReadFile()
        {
            var sourcePath = _dataSourceBaseUrl + "data_"+ Index +".txt";
            var request = WebRequest.Create(sourcePath) as HttpWebRequest;

            if (request == null)
            {
                throw new Exception("Unable to request source file.");
            }

            var response = request.GetResponse() as HttpWebResponse;

            if (response == null)
            {
                throw new Exception("Unable to get response.");
            }

            var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException("Unable to get stream."));
            var wholeContent = streamReader.ReadToEnd();
            streamReader.Close();

            return wholeContent.Split('\n');
        }

        #endregion

        #region Private methods

        private bool ParseIsActive(string line, string trueValue, string falseValue)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(trueValue) || string.IsNullOrEmpty(falseValue))
            {
                throw new Exception("Invalid input");
            }

            if (line == trueValue)
            {
                return true;
            }
            if (line == falseValue)
            {
                return false;
            }

            throw new Exception("Unable to define state.");
        }

        private string ParsePhoneNumber(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            if (line.Length == 10)
            {
                var success = line.All(char.IsDigit);

                if (success)
                {
                    return line;
                }

                throw new Exception("Phone number contains letters.");
            }

            throw new Exception("Unable to define phone number.");
        }

        private Language ParseLanguage(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            switch (line)
            {
                case "E":
                    return Language.Estonian;
                case "I":
                    return Language.English;
                case " ":
                    return Language.Undefined;
                default: throw new Exception("Unable to define language.");
            }
        }

        private string ParseDateAndTime(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            var lineWithSeconds = line + "00";
            var format = "yyyyMMddHHmmss";

            var success = DateTime.TryParseExact(
                lineWithSeconds, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime);

            if (success)
            {
                return dateTime.ToString("MM/dd/yyyy HH:mm");
            }

            throw new Exception("Unable to define date and time.");
        }

        private string ParseTimeSpan(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            var lineWithSeconds = line + "00";
            var format = "hhmmss";
            TimeSpan timeSpan;

            var success = TimeSpan.TryParseExact(
                lineWithSeconds, format, CultureInfo.CurrentCulture, out timeSpan);

            if (success)
            {
                return timeSpan.ToString(@"hh\:mm");
            }

            throw new Exception("Unable to define time.");
        }

        private IEnumerable<Contact> ParseContactList(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            var phoneNumbersInFile = line.Substring(0, 120);
            var names = line.Substring(120);

            var phoneNumberList = Split(phoneNumbersInFile, 15);
            var nameList = Split(names, 20);

            if (phoneNumberList.Count() != nameList.Count())
            {
                throw new Exception("Phone number and name lists have different size.");
            }

            var listOfContacts = new List<Contact>();

            for (int i = 0; i < phoneNumberList.Count(); i++)
            {
                var numberString = phoneNumberList.ElementAt(i);

                var contact = new Contact()
                {
                    PhoneNumber = numberString.Substring(0, 10),
                    Name = nameList.ElementAt(i)
                };

                listOfContacts.Add(contact);
            }

            return listOfContacts;
        }

        private IEnumerable<string> Split(string input, int size)
        {
            if (string.IsNullOrEmpty(input) || size < 1)
            {
                throw new Exception("Unable to split due to invalid parameter.");
            }

            return Enumerable.Range(0, input.Length / size)
                .Select(i => input.Substring(i * size, size));
        }

        #endregion
    }
}
