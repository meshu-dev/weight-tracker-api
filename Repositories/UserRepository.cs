using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    #pragma warning disable CS1591
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override UserModel Create(UserModel model)
        {
            var entity = AddModel(model);

            if (Save() == true) {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public async Task<UserModel> CreateAsync(UserModel model)
        {
            var entity = AddModel(model);

            if (await SaveAsync() == true)
            {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public async Task<UserModel> CreateAsync(
            UserModel model,
            RoleModel roleModel,
            UnitModel unitModel
        ) {
            var entity = AddModel(model);

            entity.Role = mapper.Map<Role>(roleModel);
            entity.Unit = mapper.Map<Unit>(unitModel);

            if (await SaveAsync() == true)
            {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        private User AddModel(UserModel model)
        {
            model.Password = Crypto.HashPassword(model.Password);
            var entity = mapper.Map<User>(model);

            context.Add(entity);
            return entity;
        }

        public override UserModel Read(int id)
        {
            var entity = ReadQueryable(id).FirstOrDefault();

            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        public async Task<UserModel> ReadAsync(int id)
        {
            IQueryable<User> queryable = ReadQueryable(id);
            var entity = await queryable.FirstOrDefaultAsync();

            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        public async Task<UserModel> ReadAsync(int id, bool isTracked)
        {
            var entity = await context.Users.FindAsync(id);
            
            if (isTracked == false)
            {
                context.Entry(entity).State = EntityState.Detached;
            }
            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        private IQueryable<User> ReadQueryable(int id)
        {
            return context.Users
                .Include(r => r.Role)
                .Include(u => u.Unit)
                .Where(u => u.Id == id)
                .AsQueryable();
        }

        public UserModel ReadByEmail(string email)
        {
            var entity = ReadByEmailQueryable(email).FirstOrDefault();

            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        public async Task<UserModel> ReadByEmailAsync(string email)
        {
            IQueryable<User> queryable = ReadByEmailQueryable(email);
            var entity = await queryable.FirstOrDefaultAsync();

            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        private IQueryable<User> ReadByEmailQueryable(string email)
        {
            return context.Users
                .Include(r => r.Role)
                .Include(u => u.Unit)
                .Where(u => u.Email == email)
                .AsQueryable();
        }

        public override UserModel[] ReadAll()
        {
            var entities = ReadAllQueryable()
                            .ToArray();

            if (entities == null) return null;

            return mapper.Map<UserModel[]>(entities);
        }

        public async Task<UserModel[]> ReadAllAsync()
        {
            IQueryable<User> queryable = ReadAllQueryable();
            var entities = await queryable.ToArrayAsync();

            if (entities == null) return null;

            return mapper.Map<UserModel[]>(entities);
        }

        private IQueryable<User> ReadAllQueryable()
        {
            return context.Users
                .AsNoTracking()
                .Include(r => r.Role)
                .Include(u => u.Unit)
                .AsQueryable();
        }

        public override UserModel Update(UserModel model)
        {
            var entity = context.Users.Find(model.Id);
            if (entity == null) return null;

            if (model.Password != entity.Password)
            {
                model.Password = Crypto.HashPassword(model.Password);
            }

            mapper.Map(model, entity);

            if (Save() == true)
            {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public async Task<UserModel> UpdateAsync(UserModel model)
        {
            var entity = await context.Users.FindAsync(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (await SaveAsync() == true)
            {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public async Task<UserModel> UpdateAsync(
            UserModel model,
            RoleModel roleModel,
            UnitModel unitModel
        ) {
            var entity = await context.Users.FindAsync(model.Id);
            if (entity == null) return null;

            entity.Role = mapper.Map<Role>(roleModel);
            entity.Unit = mapper.Map<Unit>(unitModel);

            mapper.Map(model, entity);

            if (model.Password != entity.Password)
            {
                model.Password = Crypto.HashPassword(model.Password);
            }

            if (await SaveAsync() == true)
            {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public override bool Delete(UserModel model)
        {
            DeleteModel(model);
            return Save();
        }

        public async Task<bool> DeleteAsync(UserModel model)
        {
            DeleteModel(model);
            return await SaveAsync();
        }

        private void DeleteModel(UserModel model)
        {
            var entity = mapper.Map<User>(model);
            context.Users.Remove(entity);
        }
    }
    #pragma warning restore CS1591
}
