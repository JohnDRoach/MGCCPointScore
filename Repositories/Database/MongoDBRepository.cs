namespace Repositories.Database
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    internal class MongoDBRepository<T> : IMongoDBRepository<T> where T : IHaveAGuidId
    {
        private readonly MongoCollection<T> collection;

        public MongoDBRepository(string collectionName)
        {
            collection = MyMongoDB.Database.GetCollection<T>(collectionName);
        }

        public IEnumerable<T> AllMembers
        {
            get
            {
                return collection.FindAll();
            }
        }

        public void Add(T item)
        {
            if (item.Id != Guid.Empty)
            {
                throw new FormatException("I create the GUIDs here son! Or maybe you are trying to Add instead of Update?");
            }

            collection.Insert(item);
        }

        public void Update(T item)
        {
            if (item.Id == Guid.Empty)
            {
                throw new EmptyIdException("I cannot update something with no Id. Maybe you meant Add instead?");
            }

            GetFromId(item.Id);

            collection.Save(item);
        }

        public void Delete(T item)
        {
            if (item.Id == Guid.Empty)
            {
                throw new EmptyIdException("I cannot delete something with no Id.");
            }

            var query = Query<T>.EQ(e => e.Id, item.Id);
            collection.Remove(query);
        }

        public T GetFromId(Guid id)
        {
            var query = Query<T>.EQ(e => e.Id, id);

            try
            {
                return GetFromQuery(query);
            }
            catch(ItemNotFoundException e)
            {
                throw new ItemNotFoundException(string.Format("Item with id {0} was not found. {1}", id, e.Message));
            }
        }

        public T GetFromQuery(IMongoQuery query)
        {
            var result = collection.FindOne(query);

            if (result != null)
            {
                return result;
            }

            throw new ItemNotFoundException("Are you sure you have the right Repository?");
        }
    }
}
