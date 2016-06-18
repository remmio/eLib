using System;
using System.Collections;
using System.Collections.Generic;

namespace eLib.Collections
{
    /// <summary> A generic priority queue implementation. </summary>
    /// <remarks> David Venegoni, Jan 02 2014. </remarks>
    /// <seealso> cref="T:System.Collections.Generic.IEnumerable{T}". </seealso>
    /// <seealso> cref="T:Utilities.Interfaces.IPriorityQueue{T}". </seealso>
    /// <typeparam name="T"> Generic type parameter, where T 
    ///                      must implement the IComparable interface. </typeparam>
    [Serializable]
    public class PriorityQueue<T> : IEnumerable<T>, IPriorityQueue<T>
        where T : IComparable<T>
    {
        #region Private Member Variables

        private readonly List<T> _items; /* The items in the queue */

        #endregion

        #region Properties

        /// <summary> Gets the convention this priority queue uses to sort and insert items. </summary>
        /// <value> The ordering convention. </value>
        public PriorityConvention OrderingConvention { get; private set; }

        /// <summary> Gets the number of items that are in the priority queue. </summary>
        /// <value> The number of items in the priority queue. </value>
        public int Count => _items.Count;

        #endregion

        #region Constructors

        /// <summary> Initializes a new instance of the PriorityQueue class. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="ArgumentException"> Thrown when the convention is specified 
        ///                                      as PriorityConvention.None. </exception>
        /// <param name="convention">
        ///     (Optional) the convention to use when sorting and inserting items (this cannot be changed
        ///     after the priority queue is created).
        /// </param>
        public PriorityQueue(PriorityConvention convention = PriorityConvention.HighestPriorityInFront)
        {
            if (convention == PriorityConvention.None)
                throw new ArgumentException("No valid ordering convention was specified", nameof(convention));

            OrderingConvention = convention;
            _items = new List<T>();
        }

        /// <summary> Initializes a new instance of the PriorityQueue class. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="ArgumentException"> Thrown when the convention is specified
        ///                                       as PriorityConvention.None. </exception>
        /// <param name="priorityItems"> The items to initialize the priority queue with. </param>
        /// <param name="convention">
        ///     (Optional) the convention to use when sorting and inserting items (this cannot be changed
        ///     after the priority queue is created).
        /// </param>
        public PriorityQueue(IEnumerable<T> priorityItems,
            PriorityConvention convention = PriorityConvention.HighestPriorityInFront)
            : this(convention)
        {
            if (convention == PriorityConvention.None)
                throw new ArgumentException("No valid ordering convention was specified", nameof(convention));

            AddRange(priorityItems);
        }

        #endregion

        #region Public Methods

        /// <summary> Gets the enumerator for the priority queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <returns> The enumerator for the priority queue. </returns>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

        /// <summary> Gets the enumerator for the priority queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <returns> The enumerator for the priority queue. </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary> Adds an item to the priority queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="item"> The item to add. </param>
        public void Add(T item) => Insert(item);

        /// <summary>
        ///     Adds the items to the priority queue.  This method checks if the enumerable is null 
        ///     and only iterates of the items once.
        /// </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="itemsToAdd"> An IEnumerable&lt;T&gt; of items to add to the priority queue. </param>
        public void AddRange(IEnumerable<T> itemsToAdd)
        {
            if (itemsToAdd == null)
                return;

            foreach (var item in itemsToAdd)
                Insert(item);
        }

        /// <summary> Clears all the items from the priority queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        public void Clear() => _items.Clear();

        /// <summary> Clears all the items starting at the specified start index. </summary>
        /// <remarks> David Venegoni, Jan 03 2014. </remarks>
        /// <param name="startIndex"> The start index. </param>
        /// <returns> The number of items that were removed from the priority queue. </returns>
        public int Clear(int startIndex)
        {
            var numberOfItems = _items.Count - 1 - startIndex;
            _items.RemoveRange(startIndex, numberOfItems);
            return numberOfItems;
        }

        /// <summary> Clears the number of items specified by count 
        ///           from the priority queue starting at specified start index. </summary>
        /// <remarks> David Venegoni, Jan 03 2014. </remarks>
        /// <param name="startIndex"> The start index. </param>
        /// <param name="count">      Number of items to remove. </param>
        public void Clear(int startIndex, int count) => _items.RemoveRange(startIndex, count);

        /// <summary> Clears all the items that satisfy the specified predicate function. </summary>
        /// <remarks> David Venegoni, Jan 03 2014. </remarks>
        /// <param name="predicateFunction"> The predicate function to use in determining 
        ///                                  which items should be removed. </param>
        /// <returns> The number of items that were removed from the priority queue. </returns>
        public int ClearWhere(Func<T, bool> predicateFunction) => _items.RemoveAll(predicateFunction.Invoke);

        /// <summary> Pops an item from the front of the queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="InvalidOperationException"> Thrown when no items exist 
        ///                                              in the priority queue. </exception>
        /// <returns> An item from the front of the queue. </returns>
        public T PopFront()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("No elements exist in the queue");

            var item = _items[0];
            _items.RemoveAt(0);
            return item;
        }

        /// <summary> Pops an item from the back of the queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="InvalidOperationException"> Thrown when no items exist
        ///                                              in the priority queue. </exception>
        /// <returns> An item from the back of the queue. </returns>
        public T PopBack()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("No elements exist in the queue");

            var tail = _items.Count - 1;
            var item = _items[tail];
            _items.RemoveAt(tail);
            return item;
        }

        /// <summary> Peeks at the item at the front queue without removing the item. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="InvalidOperationException"> Thrown when no items exist 
        ///                                              in the priority queue. </exception>
        /// <returns> The item at the front of the queue. </returns>
        public T PeekFront()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("No elements exist in the queue");

            return _items[0];
        }

        /// <summary> Peeks at the item at the back of the queue without removing the item. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="InvalidOperationException"> Thrown when no items exist 
        ///                                              in the priority queue. </exception>
        /// <returns> The item at the back of the queue. </returns>
        public T PeekBack()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("No elements exist in the queue");

            return _items[_items.Count - 1];
        }

        /// <summary> Pops the specified number of items from the front of the queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="ArgumentException">
        ///     Thrown when the number of items to pop exceeds the number of items in the priority
        ///     queue.
        /// </exception>
        /// <param name="numberToPop"> Number of items to pop from the front of the queue. </param>
        /// <returns> The items from the front of the queue. </returns>
        public IEnumerable<T> PopFront(int numberToPop)
        {
            if (numberToPop > _items.Count)
                throw new ArgumentException(@"The numberToPop exceeds the number 
                                              of elements in the queue", nameof(numberToPop));

            var poppedItems = new List<T>();
            while (poppedItems.Count < numberToPop)
                poppedItems.Add(PopFront());

            return poppedItems;
        }

        /// <summary> Pops the specified number of items from the back of the queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <exception cref="ArgumentException">
        ///     Thrown when the number of items to pop exceeds the number of items in the priority
        ///     queue.
        /// </exception>
        /// <param name="numberToPop"> Number of items to pop from the back of the queue. </param>
        /// <returns> The items from the back of the queue. </returns>
        public IEnumerable<T> PopBack(int numberToPop)
        {
            if (numberToPop > _items.Count)
                throw new ArgumentException(@"The numberToPop exceeds the number 
                                              of elements in the queue", nameof(numberToPop));

            var poppedItems = new List<T>();
            while (poppedItems.Count < numberToPop)
                poppedItems.Add(PopBack());

            return poppedItems;
        }

        /// <summary> Queries if the priority queue is empty. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <returns> true if the priority queue empty, false if not. </returns>
        public bool IsEmpty() => _items.Count == 0;

        #endregion

        #region Private Methods

        /// <summary> Inserts the given item into the queue. </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="item"> The item to insert into the queue. </param>
        private void Insert(T item)
        {
            if (_items.Count == 0)
                _items.Add(item);
            else if (OrderingConvention == PriorityConvention.HighestPriorityInFront)
                InsertAscending(item);
            else
                InsertDescending(item);
        }

        /// <summary>
        ///     Inserts the specified item into the priority queue 
        ///     (using the PriorityConvention.HighestPriorityInFront convention).
        /// </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="item"> The item to insert into the queue. </param>
        private void InsertAscending(T item)
        {
            var tail = _items[_items.Count - 1];
            var comparedToTail = item.CompareTo(tail);

            if (comparedToTail <= 0) // Less or equal to the than the current minimum

                _items.Add(item);
            else if (_items.Count == 1)
            {
                /* 
                 * Must assume that since there is only one item
                 * in the list and that the function has reached 
                 * this point, that the current item is greater 
                 * than the item in the queue, so needs to be 
                 * inserted in front 
                 */
                _items.Insert(0, item);
            }
            else
                FindIndexAndInsertItemAscending(item);
        }

        /// <summary>
        ///     Inserts the specified item into the priority queue 
        ///     (using the PriorityConvention.LowestPriorityInFront convention).
        /// </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="item"> The item to insert into the queue. </param>
        private void InsertDescending(T item)
        {
            var tail = _items[_items.Count - 1];
            var comparedToTail = item.CompareTo(tail);

            if (comparedToTail >= 0) // Greater than or equal to current maximum

                _items.Add(item);
            else if (_items.Count == 1) // See InsertAscending for explanation

                _items.Insert(0, item);
            else
                FindIndexAndInsertItemDescending(item);
        }

        /// <summary>
        ///     Searches for the index where the given item should be place in the queue and, 
        ///     subsequently, inserts the item at the specified index.
        /// </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="item"> The item to insert into the queue. </param>
        private void FindIndexAndInsertItemAscending(T item)
        {
            var lowerBoundIndex = 0;
            var upperBoundIndex = _items.Count - 1;
            var currentMedianIndex = upperBoundIndex / 2; // No need to floor, integers will always round towards 0

            /* 
             * Will determine which side of the median the current item should be placed, 
             * then updating the lower and upper bounds accordingly until the proper index 
             * is found, at which the point the item will be inserted.
             */
            while (true)
            {
                var comparisonResult = item.CompareTo(_items[currentMedianIndex]);
                switch (comparisonResult)
                {
                    case 1:
                        upperBoundIndex = currentMedianIndex;
                        break;
                    case -1:
                        lowerBoundIndex = currentMedianIndex;
                        break;
                    default:
                        FindIndexAndInsertItem(item,
                                               currentMedianIndex,
                                               PriorityConvention.HighestPriorityInFront);
                        return;
                }

                if (AreEndConditionsMet(item, lowerBoundIndex, upperBoundIndex, ref currentMedianIndex))
                    break;
            }
        }

        /// <summary>
        ///     Searches for the index where the given item should be place in the queue and, 
        ///     subsequently, inserts the item at the specified index.
        /// </summary>
        /// <remarks> David Venegoni, Jan 02 2014. </remarks>
        /// <param name="item"> The item to insert into the queue. </param>
        private void FindIndexAndInsertItemDescending(T item)
        {
            // See FindIndexAndInsertItemAscending for explanation
            var lowerBoundIndex = 0;
            var upperBoundIndex = _items.Count - 1;
            var currentMedianIndex = upperBoundIndex / 2;

            while (true)
            {
                var comparisonResult = item.CompareTo(_items[currentMedianIndex]);
                switch (comparisonResult)
                {
                    case 1:
                        lowerBoundIndex = currentMedianIndex;
                        break;
                    case -1:
                        upperBoundIndex = currentMedianIndex;
                        break;
                    default:
                        FindIndexAndInsertItem(item,
                                               currentMedianIndex,
                                               PriorityConvention.LowestPriorityInFront);
                        return;
                }

                if (AreEndConditionsMet(item, lowerBoundIndex, upperBoundIndex, ref currentMedianIndex))
                    break;
            }
        }

        /// <summary>
        ///     Searches for the index where the given item should be place in the queue and,
        ///     subsequently, inserts the item at the specified index.
        /// </summary>
        /// <remarks>
        ///     This method will be called when the specified item equals 
        ///     another item (can be more than one) within the queue.
        ///     David Venegoni, Jan 02 2014.
        /// </remarks>
        /// <param name="item">               The item to insert into the queue. </param>
        /// <param name="currentIndex">       The index in which to start at. </param>
        /// <param name="priorityConvention"> The priority convention to use when finding the index. </param>
        private void FindIndexAndInsertItem(T item, int currentIndex, PriorityConvention priorityConvention)
        {
            var currentPosition = currentIndex;
            var condition = priorityConvention == PriorityConvention.HighestPriorityInFront ? 1 : -1;
            var isLastElement = false;
            while (item.CompareTo(_items[currentPosition]) != condition)
            {
                ++currentPosition;
                if (currentPosition < _items.Count) // Make sure the index does not go out of range

                    continue;

                isLastElement = true;
                break;
            }

            if (isLastElement)
                _items.Add(item);
            else
                _items.Insert(currentPosition, item);
        }

        /// <summary>
        ///     Determines if the current lower bound, upper bound, and median index are 
        ///     at the end conditions, if not, the current median index is updated 
        ///     using the lower and upper bound indices.
        /// </summary>
        /// <remarks>
        ///     The end conditions are:  
        ///                             1)  Is the upper bound index 0?    
        ///                             2)  Is the lower bound index the last index in the queue?    
        ///                             3)  Is the new median index (calculated using lower and 
        ///                                 upper bound indices) the same as the current median index?
        ///     David Venegoni, Jan 02 2014.
        /// </remarks>
        /// <param name="item">               The item to insert if the end conditions are met. </param>
        /// <param name="lowerBoundIndex">    Zero-based index of the lower bound. </param>
        /// <param name="upperBoundIndex">    Zero-based index of the upper bound. </param>
        /// <param name="currentMedianIndex"> [in,out] The current median index. </param>
        /// <returns> true if end conditions met, false if not. </returns>
        private bool AreEndConditionsMet(T item, int lowerBoundIndex,
                                                int upperBoundIndex, ref int currentMedianIndex)
        {
            if (upperBoundIndex == 0)
            {
                _items.Insert(0, item);
                return true;
            }

            if (lowerBoundIndex == _items.Count - 1)
            {
                _items.Add(item);
                return true;
            }

            /* 
             * If the new median is the same as the old median, 
             * then this item's priority will always be +1 from 
             * the median's priority, not to mention that continuing 
             * to use that median will result in an infinite loop 
             */
            var newMedianIndex = (upperBoundIndex + lowerBoundIndex) / 2;
            if (currentMedianIndex == newMedianIndex)
            {
                _items.Insert(currentMedianIndex + 1, item);
                return true;
            }

            currentMedianIndex = newMedianIndex;
            return false;
        }

        #endregion
    }


    public enum PriorityConvention
    {
        None,
        HighestPriorityInFront,
        LowestPriorityInFront
    }


    /// <summary> Generic priority queue interface. </summary>
    /// <remarks> David Venegoni, Jan 02 2014. </remarks>
    /// <typeparam name="T"> Generic type parameter.  Must implement the IComparable interface. </typeparam>
    public interface IPriorityQueue<T> where T : IComparable<T>
    {
        /// <summary> Gets the number of items in the priority queue. </summary>
        /// <value> The number of items in the priority queue. </value>
        int Count { get; }

        /// <summary> Adds an item to the priority queue, inserting it with respect to its priority. </summary>
        /// <param name="item"> The item to add. </param>
        void Add(T item);

        /// <summary> Adds a range of items to the priority queue, inserting them with respect to their priority. </summary>
        /// <param name="itemsToAdd"> An IEnumerable&lt;T&gt; of items to add to the priority queue. </param>
        void AddRange(IEnumerable<T> itemsToAdd);

        /// <summary> Clears all the items from the priority queue. </summary>
        void Clear();

        /// <summary> Clears all the items starting at the specified start index. </summary>
        /// <param name="startIndex"> The start index. </param>
        /// <returns> The number of items that were removed from the priority queue. </returns>
        int Clear(int startIndex);

        /// <summary> Clears the number of items specified by count from the priority queue starting at specified start index. </summary>
        /// <param name="startIndex"> The start index. </param>
        /// <param name="count">      Number of items to remove. </param>
        void Clear(int startIndex, int count);

        /// <summary> Clears all the items that satisfy the specified predicate function. </summary>
        /// <param name="predicateFunction"> The predicate function to use in determining which items should be removed. </param>
        /// <returns> The number of items that were removed from the priority queue. </returns>
        int ClearWhere(Func<T, bool> predicateFunction);

        /// <summary> Pops an item from the front of the queue. </summary>
        /// <returns> An item from the front of the queue. </returns>
        T PopFront();

        /// <summary> Pops an item from the back of the queue. </summary>
        /// <returns> An item from the back of the queue. </returns>
        T PopBack();

        /// <summary> Peeks at the item at the front of the queue, but does not remove it from the queue. </summary>
        /// <returns> The item that is at the front of the queue. </returns>
        T PeekFront();

        /// <summary> Peek at the item at the back of the queue, but does not remove it from the queue. </summary>
        /// <returns> The item that is at the back of the queue. </returns>
        T PeekBack();

        /// <summary> Pops the specified number of items from the front of the queue. </summary>
        /// <param name="numberToPop"> Number of items to pop from the front of the queue. </param>
        /// <returns> The items that were popped from the front of the queue. </returns>
        IEnumerable<T> PopFront(int numberToPop);

        /// <summary> Pops the specified number of items from the back of the queue. </summary>
        /// <param name="numberToPop"> Number of items to pop from the back of the queue. </param>
        /// <returns> The items that were popped from the back of the queue. </returns>
        IEnumerable<T> PopBack(int numberToPop);

        /// <summary> Queries if the priority queue is empty. </summary>
        /// <returns> true if the priority queue is empty, false if not. </returns>
        bool IsEmpty();
    }
    
}