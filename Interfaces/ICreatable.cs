using System;

namespace eLib.Interfaces
{
    public interface ICreatable
    {
        Guid? CreatedBy { get; set; }
        DateTime? DateCreated { get; set; }
    }
}
