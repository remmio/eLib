using System;
using eLib.Enums;

namespace eLib.Interfaces
{
    /// <summary>
    /// Implementation de system d'approuvation
    /// </summary>
    public interface IApprovable
    {
    
        /// <summary>
        /// Status de la demande
        /// </summary>
        DemandStatut Demand { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        string StatusMessage { get; set; }  

        /// <summary>
        /// Guid du reponder
        /// </summary>
        Guid? ResponderGuid { get; set; }

        /// <summary>
        /// Est Supprimer
        /// </summary>
        DateTime? DateResponded { get; set; }

    }
}
