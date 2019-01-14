using System.Collections.Generic;

namespace MonitoringService.Interfaces
{
    /// <summary>
    /// Implementation of IRepository interface
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>With the Items</returns>
        IEnumerable<T> GetAllItem();

        /// <summary>
        /// Gets the item
        /// </summary>
        /// <returns>With the Item</returns>
        T GetOneItem();
    }
}
