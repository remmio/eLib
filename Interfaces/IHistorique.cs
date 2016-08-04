using System;

namespace eLib.Interfaces
{
    /// <summary>
    /// Object Historique
    /// </summary>
    public interface IHistorique
    {
        /// <summary>
        /// Date d'Ajout
        /// </summary>
        DateTime? DateCreated { get; set; }

        /// <summary>
        /// Guid du de L'Ajouteur
        /// </summary>
        Guid? CreatedBy { get; set; }

        /// <summary>
        /// Date de Modification
        /// </summary>
        DateTime? DateEdited { get; set; }

        /// <summary>
        /// Guid du Modificateur
        /// </summary>
        Guid? EditedBy { get; set; }
    }
}
