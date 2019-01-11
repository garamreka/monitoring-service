using System;
using System.Collections.Generic;

namespace MonitoringService.Models
{
    /// <summary>
    /// Implementation of ServiceParameter class
    /// </summary>
    public class ServiceParameter
    {
        /// <summary>
        /// The sequence number of the request
        /// </summary>
        public int RequestSequenceNumber { get; set; }

        /// <summary>
        /// The associated phone number
        /// </summary>
        public int PhoneNumber { get; set; }

        /// <summary>
        /// The state of the service
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The expiry date of service
        /// </summary>
        public DateTime ExpiryDateAndTime { get; set; }

        /// <summary>
        /// The service language
        /// </summary>
        public string ServiceLanguage { get; set; }

        /// <summary>
        /// The language of XL service
        /// </summary>
        public string XlServiceLanguage { get; set; }

        /// <summary>
        /// The activation time of XL service
        /// </summary>
        public TimeSpan XlServiceActivationTime { get; set; }

        /// <summary>
        /// The end time of XL service
        /// </summary>
        public TimeSpan XlServiceEndTime { get; set; }

        /// <summary>
        /// The list of contacts
        /// </summary>
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
