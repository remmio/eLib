using System.ComponentModel.DataAnnotations;
using eLib.Properties;

namespace eLib.Enums
{
    public enum DemandStatut
    {
        [Display(Description = "Waiting", ResourceType = typeof (Resources))]
        Waiting,

        [Display(Description = "Accepted", ResourceType = typeof (Resources))]
        Accepted,

        [Display(Description = "Rejected", ResourceType = typeof (Resources))]
        Rejected
    }
}
