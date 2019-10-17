using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    public class DataController<T> : Controller where T : IModel
    {
        protected readonly Repository<T> repository;

        public DataController(Repository<T> repository)
        {
            this.repository = repository;
        }
    }
}
