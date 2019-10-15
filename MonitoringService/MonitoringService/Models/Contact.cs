using System;

namespace MonitoringService.Models
{
    /// <summary>
    /// Implementation of Contact class
    /// </summary>
    public class Contact
    {
        #region Public properties

        /// <summary>
        /// The phone number of the contact person
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The name of the contact person
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Overrides

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var toCompareWith = obj as Contact;

            if (toCompareWith == null)
            {
                return false;
            }

            return PhoneNumber == toCompareWith.PhoneNumber &&
                   Name == toCompareWith.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PhoneNumber, Name);
        }

        #endregion
    }
}
