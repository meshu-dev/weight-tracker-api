using AutoMapper;
using WeightTracker.Api.Migrations;

namespace WeightTracker.Api.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        public BaseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Create<T>(T entity) where T : class
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
