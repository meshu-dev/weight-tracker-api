using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Entities;
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
            var entity = context.WeighIns.Find(id);

            if (entity == null) return null;

            return mapper.Map<WeighInModel>(entity);
        }

        public override WeighInModel[] ReadAll()
        {
            var entities = context.WeighIns.ToArray();

            if (entities == null) return null;

            return mapper.Map<WeighInModel[]>(entities);
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
