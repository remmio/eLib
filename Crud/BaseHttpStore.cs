using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eLib.Entity;
using eLib.Exceptions;
using eLib.Utils;

namespace eLib.Crud
{
    public class BaseHttpStore<T> : IRepository<T> where T : BaseEntity
    {
        public event EventHandler<EventArgs<T>> Added;
        public event EventHandler<CancelEventArgs<T>> Adding;
        public event EventHandler<EventArgs<object>> Deleted;
        public event EventHandler<CancelEventArgs<object>> Deleting;
        public event EventHandler<EventArgs<T>> Updated;
        public event EventHandler<CancelEventArgs<T>> Updating;

        private readonly string _path;

        public BaseHttpStore(string path)
        {
            _path = path;
        }


        public async Task<Operation> Add(T entity) => await HttpHelper.HttpAdd(entity, _path);

        public async Task<Operation> Update(T entity) => await HttpHelper.HttpUpdate(entity, _path);

        public async Task<Operation> Delete(Guid id) => await HttpHelper.HttpDelete(_path + "/" + id);

        public async Task<T> ByGuid(Guid id) => await HttpHelper.HttpGet<T>(_path + "/" + id);

        public async Task<IEnumerable<T>> GetAll() => await HttpHelper.HttpGet<IEnumerable<T>>(_path + "/GetAll");
    }
}
