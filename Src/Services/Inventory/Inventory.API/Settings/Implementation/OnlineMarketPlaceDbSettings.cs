using Inventory.API.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Settings.Implementation
{
    public class OnlineMarketPlaceDbSettings : IOnlineMarketPlaceDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ProductCollectionName { get; set; }
        public string SequenceCollectionName { get; set; }
    }
}
