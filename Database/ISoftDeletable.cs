using System;



namespace CLib.Database
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
        Guid DeletedByUserGuid { get; set; }


        /// <summary>
        /// Date de Supression
        /// </summary>
        DateTime? DateDeleted { get; set; }

    }
}
