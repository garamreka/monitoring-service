using System;
using System.ComponentModel.DataAnnotations;
using MonitoringService.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonitoringService.Models
{
    /// <summary>
    /// Implementation of ServiceParameter class
    /// </summary>
    public class Service
    {
        #region Public properties

        /// <summary>
        /// The sequence number of the request
        /// </summary>
        [Required]
        public int RequestSequenceId { get; set; }

        /// <summary>
        /// The associated phone number
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The state of the service
        /// </summary>
        [Required]
        public bool IsActive { get; set; }

        /// <summary>
        /// The expiry date of service
        /// </summary>
        public string ExpiryDateAndTime { get; set; }

        /// <summary>
        /// The service language
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Language ServiceLanguage { get; set; }

        /// <summary>
        /// The state of the XL service
        /// </summary>
        public bool IsXlServiceActive { get; set; }

        /// <summary>
        /// The XL service
        /// </summary>
        public XlService XlService { get; set; }

        #endregion

        #region Overrides

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var toCompareWith = obj as Service;

            if (toCompareWith == null)
            {
                return false;
            }

            return RequestSequenceId == toCompareWith.RequestSequenceId &&
                   PhoneNumber == toCompareWith.PhoneNumber &&
                   IsActive == toCompareWith.IsActive &&
                   ExpiryDateAndTime == toCompareWith.ExpiryDateAndTime &&
                   ServiceLanguage == toCompareWith.ServiceLanguage &&
                   IsXlServiceActive == toCompareWith.IsXlServiceActive &&
                   XlService == toCompareWith.XlService;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RequestSequenceId, PhoneNumber, IsActive, ExpiryDateAndTime, ServiceLanguage, IsXlServiceActive, XlService);
        }

        #endregion
    }
}
