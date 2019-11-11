using System.Threading.Tasks;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    #pragma warning disable CS1591
    public interface IRepository<T> where T : IModel
    {
        public T Create(T model);
        public T Read(int id);
        //public Task<T> ReadAsync(int id);
        public T[] ReadAll();
        //public Task<T> ReadAllAsync();
        public T Update(T model);
        public bool Delete(T model);
    }
    #pragma warning restore CS1591
}
