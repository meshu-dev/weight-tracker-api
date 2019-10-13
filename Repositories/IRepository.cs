using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public interface IRepository<T> where T : IModel
    {
        public T Create(T model);
        public T Read(int id);
        public T[] ReadAll();
        public T Update(T model);
        public bool Delete(T model);
    }
}
