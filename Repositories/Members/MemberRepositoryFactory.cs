namespace Repositories.Members
{
    using Repositories.Database;

    public static class MemberRepositoryFactory
    {
        public static IMemberRepository Create()
        {
            return new MemberRepository(new MongoDBRepository<ClubMember>("ClubMembers"));
        }
    }
}
