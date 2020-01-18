using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeightTracker.Api.Helpers;
using WeightTracker.Api.Helpers.ListParams;
using WeightTracker.Api.Models;
using WeightTracker.Api.Repositories;
using WeightTracker.Api.Services;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// Used to retrieve, create and update weigh-in measurements
    /// </summary>
    [ApiController]
    [Route("weighins")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class WeighInsController : Controller
    {
        /// <summary>
        /// Manages weigh-ins in data store
        /// </summary>
        protected readonly WeighInRepository weighInRepository;

        /// <summary>
        /// Manages users in data store
        /// </summary>
        protected readonly UserRepository userRepository;

        protected readonly JwtUserService jwtUserService;

        /// <summary>
        /// Converts weight units
        /// </summary>
        protected readonly UserUnitConverter userUnitConverter;

        /// <summary>
        /// Contructor used to create weigh-in controller
        /// </summary>
        public WeighInsController(
            Repository<WeighInModel> weighInRepository,
            Repository<UserModel> userRepository,
            JwtUserService jwtUserService,
            UserUnitConverter userUnitConverter
        ) {
            this.weighInRepository = (WeighInRepository) weighInRepository;
            this.userRepository = (UserRepository) userRepository;
            this.jwtUserService = jwtUserService;
            this.userUnitConverter = userUnitConverter;
        }

        /// <summary>
        /// Create a weigh-in
        /// </summary>
        /// <param name="model">The weigh-in to create</param>
        /// <returns>An ActionResult of type WeighIn</returns>
        /// <response code="422">Validation error</response>
        [HttpPost()]
        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> Post(WeighInModel model)
        {
            try
            {
                /*
                // Validation checks
                var user = await userRepository.ReadAsync(model.UserId);
                if (user == null) return BadRequest("User does not exist with provided Id");

                // Verify that submitted User ID matches logged in User ID for standard users
                if (jwtUserService.isStandardUser(HttpContext) == true && jwtUserService.verifyUserId(HttpContext, user.Id) == false)
                {
                    return this.StatusCode(StatusCodes.Status422UnprocessableEntity, new { message = "User id must match currently authenticated user id" });
                } */

                if (jwtUserService.isStandardUser(HttpContext) == true)
                {
                    // Add User ID for standard user
                    model.UserId = jwtUserService.getUserId(HttpContext);
                }
                else
                {
                    // Verify User ID for admin user
                    var user = await userRepository.ReadAsync(model.UserId);
                    if (user == null) return BadRequest("User does not exist with provided Id");
                }

                // Convert to base unit
                //model.Value = userUnitConverter.ConvertToBaseUnit(user.UnitName, model.Value);

                // Save
                var weighIn = await weighInRepository.CreateAsync(model);
                if (weighIn == null) return BadRequest("Weigh in could not be created");

                return this.StatusCode(StatusCodes.Status201Created, weighIn);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Get a weigh-in by id
        /// </summary>
        /// <param name="id">The id of the weigh-in</param>
        /// <returns>The weigh-in matching the id</returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var weighIn = await weighInRepository.ReadAsync(id);
                if (weighIn == null) return NotFound($"Weigh in does not exist with Id {id}");

                if (jwtUserService.isStandardUser(HttpContext) == true && jwtUserService.verifyUserId(HttpContext, weighIn.UserId) == false)
                {
                    return this.StatusCode(StatusCodes.Status422UnprocessableEntity, new { message = "Weigh In does not belong to authenticated user id" });
                }

                return Ok(weighIn);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting weigh in");
            }
        }

        /// <summary>
        /// Get a list of weigh-ins
        /// </summary>
        /// <returns>Multiple weigh-ins</returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> GetAll([FromQuery] WeighInListParams listParams)
        {
            try
            {
                int totalCount;

                if (jwtUserService.isStandardUser(HttpContext) == true)
                {
                    listParams.UserId = jwtUserService.getUserId(HttpContext);
                    totalCount = weighInRepository.getTotalForUser(listParams.UserId);
                }
                else
                {
                    totalCount = weighInRepository.getTotal();
                }
                this.Response.Headers.Add("X-Total-Count", totalCount.ToString());

                var weighIns = await weighInRepository.ReadAllAsync(listParams);
                if (weighIns == null) return NotFound($"No weigh ins are available");

                return Ok(weighIns);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, "There was an issue getting weigh ins");
            }
        }

        /// <summary>
        /// Update a weigh-in by id
        /// </summary>
        /// <param name="id">The id of the weigh-in</param>
        /// <param name="model">The weigh-in data to update</param>
        /// <returns>An ActionResult of type WeighIn</returns>
        /// <response code="422">Validation error</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> Put(int id, WeighInModel model)
        {
            try
            {
                /*
                var user = await userRepository.ReadAsync(model.UserId);
                if (user == null) return BadRequest("User does not exist with provided Id");

                if (jwtUserService.isStandardUser(HttpContext) == true && jwtUserService.verifyUserId(HttpContext, user.Id) == false)
                {
                    return this.StatusCode(StatusCodes.Status422UnprocessableEntity, new { message = "User id is invalid" });
                } */

                var weighIn = await weighInRepository.ReadAsync(id);
                if (weighIn == null) return NotFound($"Weigh in doesn't exist with Id {id}");

                model.Id = id;
                
                if (jwtUserService.isStandardUser(HttpContext) == true)
                {
                    // Add User ID for standard user
                    model.UserId = jwtUserService.getUserId(HttpContext);
                }
                else
                {
                    // Verify User ID for admin user
                    var user = await userRepository.ReadAsync(model.UserId);
                    if (user == null) return BadRequest("User does not exist with provided Id");
                }

                weighIn = await weighInRepository.UpdateAsync(model);
                if (weighIn == null) return BadRequest("Weigh in could not be updated");

                return Ok(weighIn);
            }
            catch (Exception e)
            {
                //throw e;
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        /// <summary>
        /// Delete a weigh-in by id
        /// </summary>
        /// <param name="id">The id of the weigh-in</param>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> Delete(int id)
        {
            var weighIn = await weighInRepository.ReadAsync(id, false);
            if (weighIn == null) return NotFound();

            if (jwtUserService.isStandardUser(HttpContext) == true && jwtUserService.verifyUserId(HttpContext, weighIn.UserId) == false)
            {
                return this.StatusCode(StatusCodes.Status422UnprocessableEntity, new { message = "Weigh In does not belong to authenticated user id" });
            }

            var isDeleted = await weighInRepository.DeleteAsync(weighIn);
            if (isDeleted == true) return NoContent();

            return NotFound("Couldn't delete Weigh in");
        }
    }
}
