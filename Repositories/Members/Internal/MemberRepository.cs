using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Repositories.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Members
{
    internal class MemberRepository : IMemberRepository
    {
        private readonly IMongoDBRepository<ClubMember> repository;
        //private const string CollectionName = "ClubMembers";
        //private readonly MongoCollection<ClubMember> collection;

        public MemberRepository(IMongoDBRepository<ClubMember> repository)
        {
            this.repository = repository;
            //collection = MyMongoDB.Database.GetCollection<ClubMember>(CollectionName);
        }

        public IEnumerable<ClubMember> AllMembers
        {
            get
            {
                return repository.AllMembers;
                //return collection.FindAll();
            }
        }

        public void Add(ClubMember member)
        {
            var query = Query<ClubMember>.EQ(e => e.Name, member.Name);

            try
            {
                repository.GetFromQuery(query);
                throw new DuplicateMemberNameException(member.Name);
            }
            catch (ItemNotFoundException)
            {
                repository.Add(member);
            }

            //if (member.Id != Guid.Empty)
            //{
            //    throw new FormatException("I create the GUIDs here son! Or maybe you are trying to Add instead of Update?");
            //}

            //var query = Query<ClubMember>.EQ(e => e.Name, member.Name);

            //if (collection.FindOne(query) == null)
            //{
            //    collection.Insert(member);
            //}
            //else
            //{
            //    throw new DuplicateMemberNameException(member.Name);
            //}
        }

        public void Update(ClubMember member)
        {
            var query = Query.And(
                Query<ClubMember>.EQ(e => e.Name, member.Name),
                Query<ClubMember>.NE(e => e.Id, member.Id));

            try
            {
                repository.GetFromQuery(query);
                throw new DuplicateMemberNameException(member.Name);
            }
            catch (ItemNotFoundException)
            {
                repository.Update(member);
            }

            //if (member.Id == Guid.Empty)
            //{
            //    throw new EmptyIdException("");
            //}

            //var query = Query.And(
            //    Query<ClubMember>.EQ(e => e.Name, member.Name),
            //    Query<ClubMember>.NE(e => e.Id, member.Id));

            //if (collection.FindOne(query) == null)
            //{
            //    collection.Save(member);
            //}
            //else
            //{
            //    throw new DuplicateMemberNameException(member.Name);
            //}
        }

        public void Delete(ClubMember member)
        {
            repository.Delete(member);

            //if (member.Id == Guid.Empty)
            //{
            //    throw new EmptyIdException("");
            //}

            //var query = Query<ClubMember>.EQ(e => e.Id, member.Id);
            //collection.Remove(query);
        }

        public ClubMember GetFromId(Guid id)
        {
            return repository.GetFromId(id);

            //var query = Query<ClubMember>.EQ(e => e.Id, id);

            //var result = collection.FindOne(query);

            //if (result != null)
            //{
            //    return result;
            //}

            //throw new MemberNotFoundException();
        }
    }
}
