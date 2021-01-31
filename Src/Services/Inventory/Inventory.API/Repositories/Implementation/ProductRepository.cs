using Inventory.API.Entities;
using Inventory.API.Repositories.Interfaces;
using MongoDB.Driver;
using Inventory.API.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.API.Common;

namespace Inventory.API.Repositories.Implementation
{
    /// <summary>
    /// DAL
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly IInventoryContext _context;

        public ProductRepository(IInventoryContext inventoryContext)
        {
            _context = inventoryContext ?? throw new ArgumentNullException(nameof(inventoryContext));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                            .Products
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<Product> GetProduct(string code)
        {
            int id;
            bool codeValid = int.TryParse(code, out id);
            if (codeValid)
            {
                var product = await _context
                            .Products
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();
                if (product != null)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetProductByProductCode(string code)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Id.ToString(), code);

            return await _context
                          .Products
                          .Find(filter)
                          .ToListAsync();
        }

        public async Task<Product> Create(Product product)
        {
            // Increment id.
            product.Id = (int)(await _context.Sequence
                            .FindOneAndUpdateAsync(Builders<Sequence>.Filter.Eq(a => a.Name, Constants.ProductIdName),
                                                   Builders<Sequence>.Update.Inc(a => a.Value, 1))).Value + 1;
            await _context.Products.InsertOneAsync(product);
            return product;

        }

        public async Task<bool> Update(Product product)
        {
            var updateResult = await _context
                            .Products
                            .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(int id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Products
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
