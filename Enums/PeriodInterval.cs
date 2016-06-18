using System.ComponentModel.DataAnnotations;
using eLib.Properties;

namespace eLib.Enums
{
   
    public enum PeriodInterval
    {      
        [Display(Description = "Once", ResourceType = typeof (Resources))]
        Once = 0,
        
        [Display(Description = "Monthly", ResourceType = typeof (Resources))]
        Monthly = 1,
        
        [Display(Description = "Quarterly", ResourceType = typeof (Resources))]
        Quarterly = 3,
        
        [Display(Description = "Half_Yearly", ResourceType = typeof (Resources))]
        HalfYearly = 6,
        
        [Display(Description = "Yearly", ResourceType = typeof (Resources))]
        Yearly = 12,

        [Display(Description = "Weekly", ResourceType = typeof (Resources))]
        WklySalary,

        [Display(Description = "Bi_Weekly", ResourceType = typeof (Resources))]
        BiWklySalary
    }
}
