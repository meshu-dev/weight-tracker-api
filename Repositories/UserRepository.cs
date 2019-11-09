using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            model.Password = Crypto.HashPassword(model.Password);
            var entity = mapper.Map<User>(model);

            context.Add(entity);

            if (Save() == true) {
                return mapper.Map<UserModel>(entity);
            }
            return null;
        }

        public override UserModel Read(int id)
        {
            var entity = context.Users
                .Include(u => u.Unit)
                .Where(u => u.Id == id)
                .FirstOrDefault();

            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        public UserModel ReadByEmail(string email)
        {
            var entity = context.Users
                .Include(u => u.Unit)
                .Where(u => u.Email == email)
                .FirstOrDefault();

            if (entity == null) return null;

            return mapper.Map<UserModel>(entity);
        }

        public override UserModel[] ReadAll()
        {
            var entities = context.Users
                .AsNoTracking()
                .Include(u => u.Unit)
                .ToArray();

            if (entities == null) return null;

            return mapper.Map<UserModel[]>(entities);
        }

        public override UserModel Update(UserModel model)
        {
            var entity = context.Users.Find(model.Id);
            if (entity == null) return null;

            mapper.Map(model, entity);

            if (model.Password != entity.Password)
            {
                model.Password = Crypto.HashPassword(model.Password);
            }

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
    #pragma warning restore CS1591
}
