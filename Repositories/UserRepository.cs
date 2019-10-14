using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override UserModel Create(UserModel model)
        {
            var entity = mapper.Map<User>(model);

            context.Add(entity);

            if (Save() == true) {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public override UserModel Read(int id)
        {
            var entity = context.Users.Find(id);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<UserModel>(entity);
        }

        public UserModel ReadByEmail(string email)
        {
            var entity = context.Users
                                .Where(u => u.Email == email)
                                .Select(u => u);

            if (entity == null) return null;

            context.Entry(entity).State = EntityState.Detached;
            return mapper.Map<UserModel>(entity);
        }

        public override UserModel[] ReadAll()
        {
            var entities = context.Users.AsNoTracking().ToArray();

            if (entities == null) return null;

            return mapper.Map<UserModel[]>(entities);
        }

        public override UserModel Update(UserModel model)
        {
            var entity = context.Users.Find(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (Save() == true)
            {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public override bool Delete(UserModel model)
        {
            var entity = mapper.Map<User>(model);

            context.Users.Remove(entity);
            return Save();
        }
    }
}
