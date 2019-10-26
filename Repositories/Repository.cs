using AutoMapper;
using System.Linq;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;
using System.Linq.Dynamic.Core;
using WeightTracker.Api.Entities;

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

        public IQueryable<IEntity> ApplyListParams(IQueryable<IEntity> queryable, ListParams listParams)
        {
            queryable = queryable.OrderBy(listParams.Sort);

            int offset = (listParams.Page - 1) * listParams.Count;
            queryable = queryable.Skip(offset).Take(listParams.Count);

            return queryable;
        }

        public bool Save()
        {
            int result = context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
