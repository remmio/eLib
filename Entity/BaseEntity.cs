using System;
using eLib.Interfaces;

namespace eLib.Entity
{
    /// <summary>
    /// Objet non Supprimable et garde les traces de modification
    /// </summary>
    public abstract class BaseEntity : BindableBase, ICreatable, IEditable, ISoftDeletable
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public Guid? EditedBy { get; set; }
        public DateTime? DateEdited { get; set; }

        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
