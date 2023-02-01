using System;

namespace Challenger.Domain.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }

        public Guid CorrelationId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double? Height { get; set; }

        public string Sex { get; set; }

        public string Avatar { get; set; }
    }
}
