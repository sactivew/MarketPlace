using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.API.Entities;

namespace Inventory.API.Data.Interfaces
{
    public interface IInventoryContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Sequence> Sequence { get; }
    }
}
