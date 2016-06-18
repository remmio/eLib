using System;

namespace eLib.Crud
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T item)
        {
            Item = item;
        }

        public EventArgs(T item, Crud cause)
        {
            Item = item;
            Cause = cause;
        }

        public T Item { get; }
        public Crud Cause { get; }
    }

    public class CrudArgs : EventArgs
    {
        public CrudArgs()
        {
            
        }
        public CrudArgs(Guid itemGuid, Crud cause, Guid notifyGuid, int count = 0)
        {
            ItemGuid = itemGuid;
            Cause = cause;
            NotifyGuid = notifyGuid;
            Count = count;
        }
            
        public Guid NotifyGuid { get; set; }
        public Guid ItemGuid { get; set; }
        public Crud Cause { get; set; }
        public int Count { get; set; }
    }

}
