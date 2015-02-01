using System;



namespace CLib.Database
{
    /// <summary>
    /// Objet non Supprimable et garde les traces de modification
    /// </summary>
    public abstract class Tracable : IHistorique, ISoftDeletable
    {

        /// <summary>
        /// Objet Tracable
        /// </summary>
        /// <returns></returns>
        public Guid AddUserGuid { get; set; }


        /// <summary>
        /// Date d'Ajout
        /// </summary>
        public DateTime? DateAdded { get; set; }


        /// <summary>
        /// Date de Modification
        /// </summary>
        public DateTime? LastEditDate { get; set; }


        /// <summary>
        /// Guid du Modificateur
        /// </summary>
        public Guid LastEditUserGuid { get; set; }


        /// <summary>
        /// Est Supprimer
        /// </summary>
        public bool IsDeleted { get; set; }


        /// <summary>
        /// Guid du Suprimeur
        /// </summary>
        public Guid DeleteUserGuid { get; set; }


        /// <summary>
        /// Date de Supression
        /// </summary>
        public DateTime? DeleteDate { get; set; }
    }
}
