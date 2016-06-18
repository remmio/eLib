using System.Threading.Tasks;

namespace eLib.Interfaces
{
    public interface ICrud
    {
        Task<bool> Add<T>(T item);

        Task<bool> Update<T>(T item);

        Task<bool> Get<T>(T item);

        Task<bool> Delete<T>(T item);

        Task<bool> Search<T>(T item);        
    }
}
