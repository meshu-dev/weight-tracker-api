using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
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

        public WeighInModel[] ReadAll(ListParams listParams)
        {
            IQueryable<IEntity> queryable = context.WeighIns
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
    }
}
