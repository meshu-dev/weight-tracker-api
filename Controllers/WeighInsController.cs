using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;

namespace WeightTracker.Api.Controllers
{
    [ApiController]
    [Route("weighins")]
    public class WeighInsController : Controller
    {
        protected readonly WeighInRepository weighInRepository;
        protected readonly UserRepository userRepository;
        protected readonly UserUnitConverter userUnitConverter;

        public WeighInsController(
            Repository<WeighInModel> weighInRepository,
            Repository<UserModel> userRepository,
            UserUnitConverter userUnitConverter
        ) {
            this.weighInRepository = (WeighInRepository) weighInRepository;
            this.userRepository = (UserRepository) userRepository;
            this.userUnitConverter = userUnitConverter;
        }

        [HttpPost()]
        public IActionResult Post(WeighInModel model)
        {
            try
            {
                // Validation checks
                var user = userRepository.Read(model.UserId);
                if (user == null) return BadRequest("User does not exist with provided Id");

                // Convert to base unit
                model.Value = userUnitConverter.ConvertToBaseUnit(user.UnitName, model.Value);

                // Save
                var weighIn = weighInRepository.Create(model);
                if (weighIn == null) return BadRequest("Weigh in could not be created");

                return Ok(weighIn);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var weighIn = weighInRepository.Read(id);
                if (weighIn == null) return NotFound($"Weigh in does not exist with Id {id}");

                return Ok(weighIn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting weigh in");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var weighIns = weighInRepository.ReadAll();
                if (weighIns == null) return NotFound($"No weigh ins are available");

                return Ok(weighIns);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting weigh ins");
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, WeighInModel model)
        {
            try
            {
                var user = userRepository.Read(model.UserId);
                if (user == null) return BadRequest("User does not exist with provided Id");

                var weighIn = weighInRepository.Read(id);
                if (weighIn == null) return NotFound($"Weigh in doesn't exist with Id {id}");

                model.Id = id;

                weighIn = weighInRepository.Update(model);
                if (weighIn == null) return BadRequest("Weigh in could not be updated");

                return Ok(weighIn);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var weighIn = weighInRepository.Read(id);
            if (weighIn == null) return NotFound();

            var isDeleted = weighInRepository.Delete(weighIn);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete Weigh in");
        }
    }
}
