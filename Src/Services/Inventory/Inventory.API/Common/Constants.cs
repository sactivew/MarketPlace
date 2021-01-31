using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Common
{
    /// <summary>
    /// Constants shared across the service.
    /// </summary>
    public class Constants
    {
        #region Naming
        public const string ProductIdName = "ProductCode";
        #endregion

        #region Message
        public const string ProductDeleted = "Product with product code: {0} is successfully deleted";
        public const string ProductUpdated = "Product with product code: {0} is successfully updated";
        public const string ProductNotFound = "Product with product code: {0} is not found.";
        public const string ServerError = "The service is not available at this moment.";
        #endregion
    }
}
