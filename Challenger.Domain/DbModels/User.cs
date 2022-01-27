namespace Challenger.Domain.DbModels
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public double Height { get; set; }

        public List<GymRecord> UserGymRecords {get;set;}

        public List<FitRecord> UserFitRecords { get; set; }

        public List<Measurement> UserMeasurements { get; set; }
    }
}
