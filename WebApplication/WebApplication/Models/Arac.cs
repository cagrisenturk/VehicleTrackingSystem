using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Arac
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public DateTime Datetime { get; set; }
        public double konumX { get; set; }
        public double konumY { get; set; }
        public int aracID { get; set; }

    }
}