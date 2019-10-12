using System;

namespace WeightTracker.Api.Models
{
    public class UserModel : IModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
