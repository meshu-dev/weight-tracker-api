using AutoMapper;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Helpers.ListParams;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;
using System.Threading.Tasks;

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

        public async Task<WeighInModel> CreateAsync(WeighInModel model)
        {
            var entity = mapper.Map<WeighIn>(model);
            context.Add(entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<WeighInModel>(entity);
            }
            return null;
        }

        public override WeighInModel Read(int id)
        {
            var entity = ReadQueryable(id).FirstOrDefault();

            if (entity == null) return null;

            return mapper.Map<WeighInModel>(entity);
        }

        public async Task<WeighInModel> ReadAsync(int id)
        {
            IQueryable<WeighIn> queryable = ReadQueryable(id);
            var entity = await queryable.FirstOrDefaultAsync();

            if (entity == null) return null;

            return mapper.Map<WeighInModel>(entity);
        }

        public async Task<WeighInModel> ReadAsync(int id, bool isTracked)
        {
            var entity = await context.WeighIns.FindAsync(id);

            if (isTracked == false)
            {
                context.Entry(entity).State = EntityState.Detached;
            }
            if (entity == null) return null;

            return mapper.Map<WeighInModel>(entity);
        }

        private IQueryable<WeighIn> ReadQueryable(int id)
        {
            return context.WeighIns
                .Include(w => w.User)
                .ThenInclude(u => u.Unit)
                .Where(w => w.Id == id)
                .AsQueryable();
        }

        public override WeighInModel[] ReadAll()
        {
            var entities = ReadAllQueryable().ToArray();

            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        public WeighInModel[] ReadAll(WeighInListParams listParams)
        {
            IQueryable<WeighIn> queryable = ReadAllQueryable(listParams);

            var entities = queryable.ToArray();
            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        public async Task<WeighInModel[]> ReadAllAsync()
        {
            IQueryable<WeighIn> weighInQueryable = ReadAllQueryable();
            var entities = await weighInQueryable.ToArrayAsync();

            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        public async Task<WeighInModel[]> ReadAllAsync(WeighInListParams listParams)
        {
            IQueryable<WeighIn> queryable = ReadAllQueryable(listParams);

            var entities = await queryable.ToArrayAsync();
            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        public async Task<WeighInModel[]> ReadAllFromUserAsync(int userId, WeighInListParams listParams)
        {
            IQueryable<WeighIn> queryable = ReadAllQueryable(userId);
            queryable = this.ApplyListParams(queryable, listParams);

            var entities = await queryable.ToArrayAsync();
            if (entities == null) return null;

            return mapper.Map<UserWeighInModel[]>(entities);
        }

        private IQueryable<WeighIn> ReadAllQueryable(int userId)
        {
            return context.WeighIns
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .Include(w => w.User)
                .ThenInclude(u => u.Unit)
                .AsQueryable();
        }

        private IQueryable<WeighIn> ReadAllQueryable()
        {
            return context.WeighIns
                .AsNoTracking()
                .Include(w => w.User)
                .ThenInclude(u => u.Unit)
                .AsQueryable();
        }

        private IQueryable<WeighIn> ReadAllQueryable(WeighInListParams listParams)
        {
            IQueryable<WeighIn> queryable = ReadAllQueryable();
            queryable = this.ApplyListParams(queryable, listParams);

            return queryable;
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

        public async Task<WeighInModel> UpdateAsync(WeighInModel model)
        {
            var entity = await context.WeighIns.FindAsync(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<WeighInModel>(entity);
            }
            return null;
        }

        public override bool Delete(WeighInModel model)
        {
            DeleteModel(model);
            return Save();
        }

        public async Task<bool> DeleteAsync(WeighInModel model)
        {
            DeleteModel(model);
            return await SaveAsync();
        }

        private void DeleteModel(WeighInModel model)
        {
            var entity = mapper.Map<WeighIn>(model);
            context.WeighIns.Remove(entity);
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
