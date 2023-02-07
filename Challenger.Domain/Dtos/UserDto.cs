using Challenger.Domain.Contracts.Identity;
using System;

namespace Challenger.Domain.Dtos
{
    public class UserDto : ApplicationUser
    {
        public DateTime? DateOfBirth { get; set; }

        public double? Height { get; set; }

        public string Sex { get; set; }
    }
}
