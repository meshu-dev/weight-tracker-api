using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("units")]
    public class UnitsController : Controller
    {
        private Repository<UnitModel> _unitRepository;

        public UnitsController(Repository<UnitModel> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_unitRepository.Read(id));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_unitRepository.ReadAll());
        }
    }
}
