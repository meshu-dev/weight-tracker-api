using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [Route("units")]
    public class UnitsController : ApiController<UnitModel>
    {
        public UnitsController(Repository<UnitModel> unitRepository) : base(unitRepository) { }
    }
}
