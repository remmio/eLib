using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eLib.Entity
{
    public class Photo : BaseEntity, IDisposable, IEqualityComparer<Photo>
    {
        [Key]
        public Guid PhotoGuid { get; set; }

        public byte[] Full { get; set; }
        public byte[] Thumb { get; set; }

        public bool Equals(Photo x, Photo y) => x.PhotoGuid.Equals(y.PhotoGuid);
        public int GetHashCode(Photo obj) => obj.PhotoGuid.GetHashCode();

        public void Dispose()
        {
            Full = null;
        }
    }
}
