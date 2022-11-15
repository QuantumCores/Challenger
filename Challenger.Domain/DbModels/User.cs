namespace Challenger.Domain.DbModels
{
    public class User
    {
        public long Id { get; set; }

        public Guid CorrelationId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public double? Height { get; set; }

        public string Sex { get; set; }

        public List<GymRecord> UserGymRecords { get; set; }

        public List<FitRecord> UserFitRecords { get; set; }

        public List<Measurement> UserMeasurements { get; set; }
    }
}
