using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;

namespace MGCCPointScore.Models
{
    public class MongoTestObject
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
