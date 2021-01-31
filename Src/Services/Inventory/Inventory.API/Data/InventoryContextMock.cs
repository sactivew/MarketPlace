using Inventory.API.Common;
using Inventory.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Data
{
    public class InventoryContextMock
    {
        public static void MockUp(IMongoCollection<Product> productCollection, IMongoCollection<Sequence> sequencColleciton)
        {
            if (!productCollection.Find(p => true).Any())
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
                if (!sequencColleciton.Find(p => p.Name == Constants.ProductIdName).Any())
                {
                    sequencColleciton.InsertOneAsync(new Sequence() { Name = Constants.ProductIdName, Value = 3 });
                }
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Lavender heart",
                    Price = "9.25"
                },
                new Product()
                {
                    Id = 2,
                    Name = "Personalised cufflinks",
                    Price = "45.00"
                },
                new Product()
                {
                    Id = 3,
                    Name = "Kids T-shirt",
                    Price = "19.95"
                }
            };
        }
    }
}
