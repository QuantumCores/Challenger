namespace Challenger.Domain.Dtos
{
    public class GymRecordDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public DateTime RecordDate { get; set; }

        public string Excersize { get; set; }

        public double Weight { get; set; }

        public int Repetitions { get; set; }

        public string MuscleGroup { get; set; }
    }
}
