using System;

namespace Challenger.Domain.Dtos
{
    public class UserDto
    {
        public Guid CorrelationId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double? Height { get; set; }

        public string Sex { get; set; }
    }
}
