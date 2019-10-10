using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class WeighInRepository : BaseRepository
    {
        public WeighInRepository(DataContext context, IMapper mapper) : base(context, mapper) { }

        public WeighInModel Create(WeighInModel model)
        {
            var entity = _mapper.Map<WeighIn>(model);

            Create(entity);

            if (Save() == true)
            {
                return _mapper.Map<WeighInModel>(entity);
            }
            return null;
        }

        public WeighInModel Read(Guid id)
        {
            var entity = _context.WeighIns.Find(id);

            if (entity == null) return null;

            return _mapper.Map<WeighInModel>(entity);
        }

        public WeighInModel[] ReadAll()
        {
            var entities = _context.WeighIns.ToArray();

            if (entities == null) return null;

            return _mapper.Map<WeighInModel[]>(entities);
        }
    }
}
