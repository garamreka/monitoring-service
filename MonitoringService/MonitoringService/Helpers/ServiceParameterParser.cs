using System;
using MonitoringService.Interfaces;
using MonitoringService.Models;

namespace MonitoringService.Helpers
{
    /// <summary>
    /// Implementation of ServiceParameterParser class
    /// </summary>
    public class ServiceParameterParser : IParser
    {
        #region Public members

        /// <summary>
        /// Parses the file content to ServiceParameter
        /// </summary>
        /// <returns>With the ServiceParameter</returns>
        public ServiceParameter ParseToParameter()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads all line of a text file
        /// </summary>
        /// <returns>With the lines of the file</returns>
        public string[] ReadFile()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
