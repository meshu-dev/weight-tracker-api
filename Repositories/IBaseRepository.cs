using System.Threading.Tasks;

namespace WeightTracker.Api.Repositories
{
    interface IBaseRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool Save();
    }
}
