using System;

namespace CLib.Database
{
    /// <summary>
    /// Object Historique
    /// </summary>
    public interface IHistorique
    {

        /// <summary>
        /// Date d'Ajout
        /// </summary>
        DateTime? DateAdded { get; set; }

        /// <summary>
        /// Guid du de L'Ajouteur
        /// </summary>
        Guid AddUserGuid { get; set; }

        /// <summary>
        /// Date de Modification
        /// </summary>
        DateTime? LastEditDate { get; set; }

        /// <summary>
        /// Guid du Modificateur
        /// </summary>
        Guid LastEditUserGuid { get; set; }

    }
}
