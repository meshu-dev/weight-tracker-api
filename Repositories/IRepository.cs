using WeightTracker.Api.Entities;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public interface IRepository<T> where T : IModel
    {
        public T Create(T model);
        public T[] ReadAll();
        public T Delete(T model);
        public bool Save();
    }
}
