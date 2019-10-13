using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
