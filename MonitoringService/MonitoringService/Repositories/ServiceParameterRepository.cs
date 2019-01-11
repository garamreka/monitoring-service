using System;
using System.Collections.Generic;
using MonitoringService.Interfaces;
using MonitoringService.Models;

namespace MonitoringService.Repositories
{
    /// <summary>
    /// Implementation of ServiceParameterRepository class
    /// </summary>
    public class ServiceParameterRepository : IRepository<ServiceParameter>
    {
        #region Public members

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>With the Items</returns>
        public IEnumerable<ServiceParameter> GetAllItem() => GetAllServiceParameter();

        /// <summary>
        /// Gets the item based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>With the Item</returns>
        public ServiceParameter GetItemById(int id) => GetServiceParameterById(id);

        #endregion

        #region Private members

        private IEnumerable<ServiceParameter> GetAllServiceParameter()
        {
            throw new NotImplementedException();
        }

        private ServiceParameter GetServiceParameterById(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
