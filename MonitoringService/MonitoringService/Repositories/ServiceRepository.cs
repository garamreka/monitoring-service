using System;
using System.Collections.Generic;
using System.Linq;
using MonitoringService.Interfaces;
using MonitoringService.Models;

namespace MonitoringService.Repositories
{
    /// <summary>
    /// Implementation of ServiceParameterRepository class
    /// </summary>
    public class ServiceRepository : IRepository<Service>
    {
        #region Fields

        private readonly IParser _serviceParameterParser;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceParameterParser"></param>
        public ServiceRepository( IParser serviceParameterParser)
        {
            _serviceParameterParser = serviceParameterParser;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>With the Items</returns>
        public IEnumerable<Service> GetAllItem() => GetAllService();

        /// <summary>
        /// Gets the item based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>With the Item</returns>
        public Service GetItemById(int id) => GetServiceById(id);

        #endregion

        #region Private methods

        private IEnumerable<Service> GetAllService()
        {
            var lines = _serviceParameterParser.ReadFile();

            if (!lines.Any())
            {
                throw new Exception("Unable to get services.");
            }

            foreach (var line in lines)
            {
                yield return _serviceParameterParser.ParseToService(line);
            }
        }

        private Service GetServiceById(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException($"Invalid id: {id}. Id should be a positive integer.");
            }

            var serviceParameter = GetAllService().FirstOrDefault(item => item.RequestSequenceId == id);

            if (serviceParameter != null)
            {
                return serviceParameter;
            }

            throw new NullReferenceException($"Could not find the services based on the given id: {id}");
        }

        #endregion
    }
}
