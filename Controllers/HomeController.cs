﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WeightTracker.Api.Models;

namespace WeightTracker.Api.Controllers
{
    /// <summary>
    /// The main index controller for API
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the current status of the API
        /// </summary>
        /// <returns>The status of the API</returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok(new { Status = "Ok" });
        }

        /// <summary>
        /// Used to test out functions of API
        /// </summary>
        /// <returns>Data related to the test function used</returns>
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            //var a = Enum.GetNames(typeof(Role));
            //var a = Enum.GetName(typeof(Role), 1);

            return Ok(new { Status = "AAA" });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("test/admin")]
        public string PingAdmin()
        {
            return "Pong";
        }

        [Authorize(Roles = "Admin2")]
        [HttpGet("test/admin2")]
        public string PingAdmin2()
        {
            return "Pong";
        }
    }
}
