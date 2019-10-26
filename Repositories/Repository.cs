using AutoMapper;
using System.Linq;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;
using System.Linq.Dynamic.Core;
using WeightTracker.Api.Helpers.ListParams;

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

        public IQueryable<T2> ApplyListParams<T2>(IQueryable<T2> queryable, ListParams listParams)
        {
            if (listParams.Sort != null)
            {
                queryable = queryable.OrderBy(listParams.Sort);
            }

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
