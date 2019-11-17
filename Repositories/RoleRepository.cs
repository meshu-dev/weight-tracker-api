using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
#pragma warning disable CS1591
    public class RoleRepository : Repository<RoleModel>
    {
        public RoleRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override RoleModel Create(RoleModel model)
        {
            var entity = mapper.Map<Role>(model);

            context.Add(entity);

            if (Save() == true)
            {
                return mapper.Map<RoleModel>(entity);
            }
            return null;
        }

        public async Task<RoleModel> CreateAsync(RoleModel model)
        {
            var entity = mapper.Map<Role>(model);

            context.Add(entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<RoleModel>(entity);
            }
            return null;
        }

        public override RoleModel Read(int id)
        {
            var entity = context.Units.Find(id);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<RoleModel>(entity);
        }

        public async Task<RoleModel> ReadAsync(int id)
        {
            var entity = await context.Units.FindAsync(id);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<RoleModel>(entity);
        }

        public override RoleModel[] ReadAll()
        {
            var entities = ReadAllQueryable().ToArray();

            if (entities == null) return null;

            return mapper.Map<RoleModel[]>(entities);
        }

        public async Task<RoleModel[]> ReadAllAsync()
        {
            IQueryable<Role> queryable = ReadAllQueryable();
            var entities = await queryable.ToArrayAsync();

            if (entities == null) return null;

            return mapper.Map<RoleModel[]>(entities);
        }

        private IQueryable<Role> ReadAllQueryable()
        {
            return null;
            /*
            return context.Roles
                .AsNoTracking()
                .AsQueryable(); */
        }

        public override RoleModel Update(RoleModel model)
        {
            var entity = context.Units.Find(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (Save() == true)
            {
                return mapper.Map<RoleModel>(entity);
            }
            return null;
        }

        public async Task<RoleModel> UpdateAsync(RoleModel model)
        {
            var entity = await context.Units.FindAsync(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<RoleModel>(entity);
            }
            return null;
        }

        public override bool Delete(RoleModel model)
        {
            DeleteModel(model);
            return Save();
        }

        public async Task<bool> DeleteAsync(RoleModel model)
        {
            DeleteModel(model);
            return await SaveAsync();
        }

        private void DeleteModel(RoleModel model)
        {
            var entity = mapper.Map<Role>(model);
            //context.Roles.Remove(entity);
        }
    }
#pragma warning restore CS1591
}
