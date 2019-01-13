using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

        private readonly string _dataSourceUrl = @"https://people.proekspert.ee/ak/data_1.txt";

        /// <summary>
        /// Gives value to RequestSequenceId
        /// </summary>
        public int Id;

        #endregion

        #region Public methods

        /// <summary>
        /// Parses the file line content to ServiceParameter
        /// </summary>
        /// <returns>With the ServiceParameter</returns>
        public Service ParseToService(string line)
        {
            Id = 1;

            var service = new Service()
            {
                RequestSequenceId = Id,
                PhoneNumber = ParsePhoneNumber(line.Substring(1, 10)),
                IsActive = ParseIsActive(line.Substring(0, 1), "A", "P")
            };

            if (service.IsActive)
            {
                service.ServiceLanguage = ParseLanguage(line.Substring(22, 1));
                service.ExpiryDateAndTime = ParseDateAndTime(line.Substring(24, 12));
                service.IsXlServiceActive = ParseIsActive(line.Substring(21, 1), "J", "E");

                if (service.IsXlServiceActive)
                {
                    service.XlService = new XlService()
                    {
                        XlServiceLanguage = ParseLanguage(line.Substring(23, 1)),
                        XlServiceActivationTime = ParseTimeSpan(line.Substring(36, 4)),
                        XlServiceEndTime = ParseTimeSpan(line.Substring(40, 4)),
                        IsOverrideListInUse = ParseIsActive(line.Substring(44, 1), "K", "E")
                    };

                    if (service.XlService.IsOverrideListInUse)
                    {
                        service.XlService.Contacts = ParseContactList(line.Substring(45));
                    }
                }
            }

            Id++;
            return service;
        }

        /// <summary>
        /// Reads all line of a text file
        /// </summary>
        /// <returns>With the lines of the file</returns>
        public string[] ReadFile()
        {
            var request = WebRequest.Create(_dataSourceUrl) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;

            var streamReader = new StreamReader(response.GetResponseStream());
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

        private DateTime ParseDateAndTime(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            var lineWithSeconds = line + "00";
            var format = "yyyyMMddHHmmss";
            DateTime dateTime;

            var success = DateTime.TryParseExact(
                lineWithSeconds, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

            if (success)
            {
                return dateTime;
            }

            throw new Exception("Unable to define date and time.");
        }

        private TimeSpan ParseTimeSpan(string line)
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
                return timeSpan;
            }

            throw new Exception("Unable to define time.");
        }

        private IEnumerable<Contact> ParseContactList(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                throw new Exception("Invalid input");
            }

            var phoneNumberPattern = @"([0-9])+";
            Match phoneNumberMatch = Regex.Match(line, phoneNumberPattern);

            var namePattern = @"[A-Za-z ]+$";
            Match nameMatch = Regex.Match(line, namePattern);

            var phoneNumberList = Split(phoneNumberMatch.ToString(), 10);
            var nameList = Split(nameMatch.ToString(), 20);

            if (phoneNumberList.Count() != nameList.Count())
            {
                throw new Exception("Phone number and name lists have different size.");
            }

            var listOfContacts = new List<Contact>();

            for (int i = 0; i < phoneNumberList.Count(); i++)
            {
                var contact = new Contact()
                {
                    PhoneNumber = phoneNumberList.ElementAt(i),
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
