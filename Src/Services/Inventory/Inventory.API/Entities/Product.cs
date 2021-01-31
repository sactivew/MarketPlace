using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Entities
{
    /// <summary>
    /// Entity mapping to the product document in the db.
    /// </summary>
    public class Product
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
