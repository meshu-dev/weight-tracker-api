using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace WeightTracker.Api.Services
{
    public class JwtUserService
    {
        private readonly string AdminRole = "Admin";
        private readonly string StandardRole = "Standard";

        public string getUserData(HttpContext httpContext, string claimType)
        {
            return httpContext.User.Claims.Where(c => c.Type == claimType).FirstOrDefault().Value;
        }

        public int getUserId(HttpContext httpContext)
        {
            return System.Convert.ToInt32(this.getUserData(httpContext, ClaimTypes.NameIdentifier));
        }

        public string getUserRole(HttpContext httpContext)
        {
            return this.getUserData(httpContext, ClaimTypes.Role);
        }

        public Boolean isAdminUser(HttpContext httpContext)
        {
            return getUserRole(httpContext) == this.AdminRole;
        }

        public Boolean isStandardUser(HttpContext httpContext)
        {
            return getUserRole(httpContext) == this.StandardRole;
        }

        public Boolean verifyUserId(HttpContext httpContext, int userId)
        {
            return userId == getUserId(httpContext);
        }
    }
}
