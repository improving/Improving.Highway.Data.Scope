namespace Improving.Highway.Data.Scope
{
    using System;

    public interface IEntity
    {
        int Id { get; set; }

        DateTime Created { get; set; }
        string CreatedBy { get; set; }

        DateTime Modified { get; set; }
        string ModifiedBy { get; set; }
    }
}
