using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class UnitRepository : Repository<UnitModel>
    {
        public UnitRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public override UnitModel Create(UnitModel model)
        {
            throw new Exception("Creating units is not allowed");
        }

        public override UnitModel Read(int id)
        {
            var entity = context.Units.Find(id);

            if (entity == null) return null;

            return mapper.Map<UnitModel>(entity);
        }

        public override UnitModel[] ReadAll()
        {
            var entities = context.Units.ToArray();

            if (entities == null) return null;

            return mapper.Map<UnitModel[]>(entities);
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
