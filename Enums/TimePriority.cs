using System.ComponentModel.DataAnnotations;
using eLib.Properties;

namespace eLib.Enums
{
    public enum TimePriority
    {
        [Display(Description = "Morning", ResourceType = typeof (Resources))]
        Morning,

        [Display(Description = "Afternoon", ResourceType = typeof (Resources))]
        Afternoon,

        [Display(Description = "Evening", ResourceType = typeof (Resources))]
        Evening
    }
}
