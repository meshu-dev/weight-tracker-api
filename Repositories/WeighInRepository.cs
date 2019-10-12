using AutoMapper;
using System;
using System.Linq;
using WeightTracker.Api.Entities;
using WeightTracker.Api.Migrations;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Repositories
{
    public class WeighInRepository : IRepository<WeighInModel>
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;

        public WeighInRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public WeighInModel Create(WeighInModel model)
        {
            var entity = _mapper.Map<WeighIn>(model);

            _context.Add(entity);

            if (Save() == true)
            {
                return _mapper.Map<WeighInModel>(entity);
            }
            return null;
        }

        public WeighInModel Create(IModel model)
        {
            throw new NotImplementedException();
        }

        public WeighInModel Delete(WeighInModel model)
        {
            throw new NotImplementedException();
        }

        public WeighInModel Delete(IModel model)
        {
            throw new NotImplementedException();
        }

        public WeighInModel Read(int id)
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

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}
