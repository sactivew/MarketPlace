using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Common
{
    /// <summary>
    /// Unified Response to the API request.
    /// </summary>
    public class Response
    {
        public string message { get; set; }

        public Response(string message)
        {
            this.message = message;
        }
    }
}
