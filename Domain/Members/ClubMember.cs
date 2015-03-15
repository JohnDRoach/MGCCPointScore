using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Domain.Members
{
    public enum Sex
    {
        Other,
        Male,
        Female
    }

    public class ClubMember
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public Sex Sex { get; set; }
    }
}
