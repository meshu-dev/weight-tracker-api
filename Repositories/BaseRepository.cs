using System.Threading.Tasks;
using WeightTracker.Api.Migrations;

namespace WeightTracker.Api.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public bool Save()
        {
            int result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
