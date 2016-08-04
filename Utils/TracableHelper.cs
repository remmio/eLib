using System;
using eLib.Entity;
using eLib.Exceptions;
using eLib.Interfaces;

namespace eLib.Utils
{
    public static class TracableHelper
    {
        public static IHavingName FixLabel(this IHavingName item)
        {
            if (string.IsNullOrEmpty(item.Name))
                throw new UxException(nameof(item) + " label can not be empty");

            if (!(item is IDescription)) return item;
            if (string.IsNullOrEmpty(((IDescription) item).Description))
                ((IDescription) item).Description = item.Name;

            return item;
        }

        public static BaseEntity FixKey(this BaseEntity item)
        {
            var key = item.GetType().GetProperty(item.GetType().Name + "Guid") ??
                      item.GetType().GetProperty(item.GetType().BaseType?.Name + "Guid");

            if (key.GetValue(item) as Guid? == default(Guid))
                key.SetValue(item, Guid.NewGuid());

            return item;
        }

        public static BaseEntity Fix(this BaseEntity item)
        {
            if (item is IHavingName) item = ((IHavingName) item).FixLabel() as BaseEntity;

            item = item.FixKey() ;

            return item;
        }

        public static Guid KeyValue(this ICreatable item)
        {
            var key = item.GetType().GetProperty(item.GetType().Name + "Guid") ??
                      item.GetType().GetProperty(item.GetType().BaseType?.Name + "Guid");

            return key.GetValue(item) as Guid? ?? Guid.Empty;
        }

        public static T PropertyValue<T>(this BaseEntity item, string propertyName) where T : struct
        {
            var key = item.GetType().GetProperty(propertyName) ??
                      item.GetType().BaseType?.GetProperty(propertyName);

            return key?.GetValue(item) is T ? (T) key.GetValue(item) : default(T) ;
        }
    }
}
