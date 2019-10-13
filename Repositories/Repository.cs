using AutoMapper;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : IModel
    {
        protected readonly DataContext context;
        protected readonly IMapper mapper;

        public Repository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public abstract T Create(T model);

        public abstract T Read(int id);

        public abstract T[] ReadAll();

        public abstract T Update(T model);

        public abstract bool Delete(T model);

        public bool Save()
        {
            int result = context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
