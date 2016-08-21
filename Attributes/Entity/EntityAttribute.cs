using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eLib.Attributes.Enums;
using eLib.Entity;

namespace eLib.Attributes.Entity
{
    public class EntityAttribute
    {
        [Key]
        public Guid EntityAttributeGuid { get; set; }
        public Guid? PictureGuid { get; set; }

        public string Label { get; set; }
        public string Placeholder { get; set; }
        
        public AttributeType Type { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }


        public string TextValue { get; set; }
        public string DefaultTextValue { get; set; }
        public int MinCharValue { get; set; }
        public int MaxCharValue { get; set; }

        public DateTime? DateValue { get; set; }
        public DateTime? DefaultDateValue { get; set; }
        public DateTime? MinDateValue { get; set; }
        public DateTime? MaxDateValue { get; set; }
            
        public int IntegerValue { get; set; }
        public int DefaultIntegerValue { get; set; }
        public int MinIntegerValue { get; set; }
        public int MaxIntegerValue { get; set; }

        public decimal DecimalValue { get; set; }
        public decimal DefaultDecimalValue { get; set; }
        public decimal MinDecimalValue { get; set; }
        public decimal MaxDecimalValue { get; set; }

        public bool BoolValue { get; set; }
        public bool DefaultBoolValue { get; set; }

            
        public string PossibleValues { get; set; }
            
        

        [ForeignKey("PictureGuid")]
        public virtual Photo Picture { get; set; }        
    }
}
