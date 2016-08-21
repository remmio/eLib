using System.ComponentModel.DataAnnotations;

namespace eLib.Attributes.Enums
{
    public enum AttributeType
    {
        [Display(Description = "Text", ResourceType = typeof(Properties.Resources))]
        Text,

        [Display(Description = "Email", ResourceType = typeof(Properties.Resources))]
        Email,

        [Display(Description = "Link", ResourceType = typeof(Properties.Resources))]
        Link,
            
        [Display(Description = "Date", ResourceType = typeof (Properties.Resources))]
        Date,

        [Display(Description = "Integer", ResourceType = typeof(Properties.Resources))]
        Integer,

        [Display(Description = "Yes/No", ResourceType = typeof(Properties.Resources))]
        Boolean,

        [Display(Description = "Decimal", ResourceType = typeof(Properties.Resources))]
        Decimal,
            
        [Display(Description = "Dropdown", ResourceType = typeof (Properties.Resources))]
        Dropdown     
    }
}
