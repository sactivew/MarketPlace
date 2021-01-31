using Inventory.API.Entities;
using Inventory.API.Settings.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using Inventory.API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Data.Interfaces
{
    public class InventoryContext : IInventoryContext
    {
        public InventoryContext(IOnlineMarketPlaceDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.ProductCollectionName);
            Sequence = database.GetCollection<Sequence>(settings.SequenceCollectionName);
            InventoryContextMock.MockUp(Products, Sequence);
        }

        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Sequence> Sequence { get; }
    }
}
