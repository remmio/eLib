using System;

namespace eLib.Interfaces
{
    public interface IOwnable
    {
        Guid? OwnerGuid { get; set; }
    }
}
