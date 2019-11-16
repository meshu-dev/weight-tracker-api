using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    #pragma warning disable CS1591
    public class UnitRepository : Repository<UnitModel>
    {
        public UnitRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override UnitModel Create(UnitModel model)
        {
            var entity = mapper.Map<Unit>(model);

            context.Add(entity);

            if (Save() == true)
            {
                return mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public async Task<UnitModel> CreateAsync(UnitModel model)
        {
            var entity = mapper.Map<Unit>(model);

            context.Add(entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public override UnitModel Read(int id)
        {
            var entity = context.Units.Find(id);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<UnitModel>(entity);
        }

        public async Task<UnitModel> ReadAsync(int id)
        {
            var entity = await context.Units.FindAsync(id);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<UnitModel>(entity);
        }

        public override UnitModel[] ReadAll()
        {
            var entities = ReadAllQueryable().ToArray();

            if (entities == null) return null;

            return mapper.Map<UnitModel[]>(entities);
        }

        public async Task<UnitModel[]> ReadAllAsync()
        {
            IQueryable<Unit> queryable = ReadAllQueryable();
            var entities = await queryable.ToArrayAsync();

            if (entities == null) return null;

            return mapper.Map<UnitModel[]>(entities);
        }

        private IQueryable<Unit> ReadAllQueryable()
        {
            return context.Units
                .AsNoTracking()
                .AsQueryable();
        }

        public override UnitModel Update(UnitModel model)
        {
            var entity = context.Units.Find(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (Save() == true)
            {
                return mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public async Task<UnitModel> UpdateAsync(UnitModel model)
        {
            var entity = await context.Units.FindAsync(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<UnitModel>(entity);
            }
            return null;
        }

        public override bool Delete(UnitModel model)
        {
            DeleteModel(model);
            return Save();
        }

        public async Task<bool> DeleteAsync(UnitModel model)
        {
            DeleteModel(model);
            return await SaveAsync();
        }

        private void DeleteModel(UnitModel model)
        {
            var entity = mapper.Map<Unit>(model);
            context.Units.Remove(entity);
        }
    }
    #pragma warning restore CS1591
}
