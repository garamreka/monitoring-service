﻿using MonitoringService.Enums;
using System;
using System.Collections.Generic;

namespace MonitoringService.Models
{
    /// <summary>
    /// Implementation of XlService class
    /// </summary>
    public class XlService
    {
        /// <summary>
        /// The language of XL service
        /// </summary>
        public Language XlServiceLanguage { get; set; }

        /// <summary>
        /// The activation time of XL service
        /// </summary>
        public TimeSpan XlServiceActivationTime { get; set; }

        /// <summary>
        /// The end time of XL service
        /// </summary>
        public TimeSpan XlServiceEndTime { get; set; }

        /// <summary>
        /// Usage state of override list
        /// </summary>
        public bool IsOverrideListInUse { get; set; }

        /// <summary>
        /// The list of contacts
        /// </summary>
        public IEnumerable<Contact> Contacts { get; set; }

        #region Overrides

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var toCompareWith = obj as XlService;

            if (toCompareWith == null)
            {
                return false;
            }

            return XlServiceLanguage == toCompareWith.XlServiceLanguage &&
                   XlServiceActivationTime == toCompareWith.XlServiceActivationTime &&
                   XlServiceEndTime == toCompareWith.XlServiceEndTime &&
                   IsOverrideListInUse == toCompareWith.IsOverrideListInUse &&
                   Contacts == toCompareWith.Contacts;
        }

        #endregion
    }
}
