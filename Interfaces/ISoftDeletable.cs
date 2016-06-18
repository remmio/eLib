using System;

namespace eLib.Interfaces
{
    /// <summary>
    /// Implementation of SoftDelete
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// Est Supprimer
        /// </summary>
        bool IsDeleted { get; set; }


        /// <summary>
        /// Guid du Suprimeur
        /// </summary>
        Guid? DeletedBy { get; set; }


        /// <summary>
        /// Date de Supression
        /// </summary>
        DateTime? DateDelete { get; set; }

    }
}
