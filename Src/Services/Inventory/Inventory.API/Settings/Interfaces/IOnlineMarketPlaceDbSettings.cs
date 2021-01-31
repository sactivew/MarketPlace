using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Settings.Interfaces
{
    public interface IOnlineMarketPlaceDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ProductCollectionName { get; set; }
        string SequenceCollectionName { get; set; }
    }
}
