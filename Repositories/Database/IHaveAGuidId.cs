namespace Repositories.Database
{
    using System;

    public interface IHaveAGuidId
    {
        Guid Id { get; set; }
    }
}
