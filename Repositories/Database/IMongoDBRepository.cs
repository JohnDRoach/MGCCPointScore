namespace Repositories.Database
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;

    internal interface IMongoDBRepository<T> where T : IHaveAGuidId
    {
        IEnumerable<T> AllMembers { get; }

        void Add(T item);
        void Update(T item);
        void Delete(T item);
        T GetFromId(Guid id);
        T GetFromQuery(IMongoQuery query);
    }
}
