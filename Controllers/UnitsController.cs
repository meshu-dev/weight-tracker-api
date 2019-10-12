using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("units")]
    public class UnitsController : Controller
    {
        private IRepository<UnitModel> _unitRepository;

        public UnitsController(IRepository<UnitModel> unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //return Ok(new { Moniker = "ATL201", Name = "Atlanta Code Camp" });

            return Ok(_unitRepository.ReadAll());
        }
    }
}
