using System.Collections.Generic;
using eLib.Attributes.Entity;

namespace eLib.Attributes
{
    public interface IHaveAttributes
    {
        ICollection<EntityAttribute> Attributes { get; set; }
    }
}
