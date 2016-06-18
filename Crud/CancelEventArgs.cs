using System.ComponentModel;

namespace eLib.Crud
{
    /// <summary>
    /// Generic version of CancelEventArgs.
    /// </summary>
    /// <typeparam name="T">A type.</typeparam>
    public class CancelEventArgs<T> : CancelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelEventArgs{T}" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public CancelEventArgs(T item)
        {
            Item = item;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public T Item { get; }
    }
}
