using AutoMapper;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IModel
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        public Repository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public abstract T Create(T model);

        public abstract T[] ReadAll();

        public abstract T Delete(T model);

        public bool Save()
        {
            int result = _context.SaveChanges();
            return result > 0 ? true : false;
        }

        /*
        public UnitModel Create(T model)
        {
            var entity = _mapper.Map<Unit>(model);

            _context.Add(entity);

            if (Save() == true)
            {
                return _mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public UnitModel Read(int id)
        {
            var entity = _context.Units.Find(id);

            if (entity == null) return null;

            return _mapper.Map<UnitModel>(entity);
        }

        public UnitModel[] ReadAll()
        {
            var entities = _context.Units.ToArray();

            if (entities == null) return null;

            return _mapper.Map<UnitModel[]>(entities);
        }

        public UnitModel Delete(UnitModel model)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            int result = _context.SaveChanges();
            return result > 0 ? true : false;
        } */
    }
}
