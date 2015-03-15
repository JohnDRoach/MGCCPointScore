namespace Repositories.Members
{
    using System;
    using MongoDB.Bson.Serialization.Attributes;
    using Repositories.Database;

    public enum Sex
    {
        Other,
        Male,
        Female
    }

    public class ClubMember : IHaveAGuidId
    {
        [BsonConstructor]
        public ClubMember(string name, Sex sex, string note)
        {
            Id = Guid.Empty;
            Name = name;
            Sex = sex;
            Note = note;
        }

        // TODO: Try making this set private as well.
        public Guid Id { get; set; }

        public string Name { get; private set; }
        public Sex Sex { get; private set; }
        public string Note { get; private set; }

        public ClubMember CopyWith(string name, Sex sex, string note)
        {
            var member = new ClubMember(name, sex, note);
            member.Id = Id;
            return member;
        }
    }
}
