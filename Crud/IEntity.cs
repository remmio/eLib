using System.ComponentModel;
using eLib.Interfaces;

namespace eLib.Crud
{
    /// <summary>
    /// Entity abstraction.
    /// </summary>
    public interface IEntity : INotifyPropertyChanged, INotifyPropertyChanging, ICreatable
    {
        ///// <summary>
        ///// Gets or sets the unique identifier.
        ///// </summary>
        ///// <value>The unique identifier.</value>
        //TId Id { get; set; }

        ///// <summary>
        ///// Gets or sets the name.
        ///// </summary>
        ///// <value>The name.</value>
        //string Name { get; set; }
    }
}
