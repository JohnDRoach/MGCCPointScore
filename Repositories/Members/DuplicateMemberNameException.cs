namespace Repositories.Members
{
    using System;

    public class DuplicateMemberNameException : Exception
    {
        public DuplicateMemberNameException(string name) : base(string.Format("Member with name '{0}' already exists.", name))
        {
        }
    }
}
