using AutoMapper;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Helpers.ListParams;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    #pragma warning disable CS1591
    public class WeighInRepository : Repository<WeighInModel>
    {
        public WeighInRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override WeighInModel Create(WeighInModel model)
        {
            var entity = mapper.Map<WeighIn>(model);
            context.Add(entity);

            if (Save() == true)
            {
                return mapper.Map<WeighInModel>(entity);
            }
            return null;
        }

        public override WeighInModel Read(int id)
        {
            var entity = context.WeighIns
                .Include(w => w.User)
                .ThenInclude(u => u.Unit)
                .Where(w => w.Id == id)
                .FirstOrDefault();

            if (entity == null) return null;

            return mapper.Map<WeighInModel>(entity);
        }

        public override WeighInModel[] ReadAll()
        {
            var entities = context.WeighIns
                .AsNoTracking()
                .Include(w => w.User)
                .ThenInclude(u => u.Unit)
                .ToArray();

            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        public WeighInModel[] ReadAll(WeighInListParams listParams)
        {
            IQueryable<WeighIn> queryable = context.WeighIns
                .AsNoTracking()
                .Include(w => w.User)
                .ThenInclude(u => u.Unit)
                .AsQueryable();

            queryable = this.ApplyListParams(queryable, listParams);
            var entities = queryable.ToArray();

            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        public override WeighInModel Update(WeighInModel model)
        {
            var entity = context.WeighIns.Find(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (Save() == true)
            {
                return mapper.Map<WeighInModel>(entity);
            }
            return null;
        }

        public override bool Delete(WeighInModel model)
        {
            var entity = mapper.Map<WeighIn>(model);

            context.WeighIns.Remove(entity);
            return Save();
        }

        public IQueryable<WeighIn> ApplyListParams(IQueryable<WeighIn> queryable, WeighInListParams listParams)
        {
            if (listParams.UserId > 0)
            {
                queryable = queryable.Where(w => w.UserId == listParams.UserId);
            }
            return base.ApplyListParams<WeighIn>(queryable, listParams);
        }
    }
    #pragma warning restore CS1591
}
