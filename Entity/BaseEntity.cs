using System;
using System.Collections.Generic;
using eLib.Attributes;
using eLib.Attributes.Entity;
using eLib.Interfaces;

namespace eLib.Entity
{
    /// <summary>
    /// Objet BaseEntity non Supprimable et garde les traces de modification avec Attributes
    /// </summary>
    public abstract class BaseEntity : BindableBase, ICreatable, IEditable, ISoftDeletable, IHaveAttributes
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public Guid? EditedBy { get; set; }
        public DateTime? DateEdited { get; set; }

        public Guid? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual ICollection<EntityAttribute> Attributes { get; set; } = new HashSet<EntityAttribute>();
    }
}
