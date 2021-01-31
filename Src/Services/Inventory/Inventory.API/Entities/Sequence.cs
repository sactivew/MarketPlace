using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.API.Entities
{
    /// <summary>
    /// Used to count the current product Id so it can be incremented when new product is created.
    /// </summary>
    public class Sequence
    {
        [BsonId]
        public ObjectId _Id { get; set; }
        public string Name { get; set; }
        public long Value { get; set; }
    }
}
