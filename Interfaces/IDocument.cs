using System;
using eLib.Enums;

namespace eLib.Interfaces {
    public interface IDocument: IHavingName, IDescription, IOwnable
    {
         Guid DocumentGuid { get; set; }
         DocumentType FileType { get; set; }
         byte[] DataBytes { get; set; }
    }
}
