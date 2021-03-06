﻿using MonitoringService.Models;

namespace MonitoringService.Interfaces
{
    /// <summary>
    /// Implementation of IParser interface
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Reads all line of a text file
        /// </summary>
        /// <returns>With the lines of the file</returns>
        string[] ReadFile();

        /// <summary>
        /// Parses the file line content to Service
        /// </summary>
        /// <returns>With the Service</returns>
        Service ParseToService(string line);
    }
}
