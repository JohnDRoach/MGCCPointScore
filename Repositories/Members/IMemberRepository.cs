namespace Repositories.Members
{
    using System;
    using System.Collections.Generic;

    // TODO: Move to Repository.Interfaces Assembly
    public interface IMemberRepository
    {
        IEnumerable<ClubMember> AllMembers { get; }

        void Add(ClubMember member);
        void Update(ClubMember member);
        void Delete(ClubMember member);
        ClubMember GetFromId(Guid id);
    }
}
