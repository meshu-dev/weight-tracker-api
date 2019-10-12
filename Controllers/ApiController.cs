using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    public class ApiController<T> : Controller where T : IModel
    {
        protected readonly Repository<T> repository;

        public ApiController(Repository<T> repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(repository.Read(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(repository.ReadAll());
        }
    }
}
