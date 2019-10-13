using AutoMapper;
using System;
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

            return mapper.Map<UserModel>(entity);
        }

        public override UserModel[] ReadAll()
        {
            var entities = context.Users.ToArray();

            if (entities == null) return null;

            return mapper.Map<UserModel[]>(entities);
        }

        public override bool Delete(int id)
        {
            var entity = context.Users.Find(id);
            if (entity == null) return false;

            context.Users.Remove(entity);

            return Save();
        }
    }
}
