using Inventory.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByProductCode(string code);
        Task<Product> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);
    }
}
