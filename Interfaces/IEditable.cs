using System;

namespace eLib.Interfaces
{
    public interface IEditable
    {
        DateTime? DateEdited { get; set; }
        Guid? EditedBy { get; set; }
    }
}
