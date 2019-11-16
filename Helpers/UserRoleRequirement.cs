using Microsoft.AspNetCore.Authorization;

namespace WeightTracker.Api.Helpers
{
    public class UserRoleRequirement : IAuthorizationRequirement
    {
        public UserRoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; }
    }
}
