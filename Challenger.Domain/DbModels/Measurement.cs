namespace Challenger.Domain.DbModels
{
    public class Measurement
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }

        public DateTime MeasurementDate { get; set; }

        public double Weight { get; set; }

        public double? Waist{ get; set; }

        public double? Neck { get; set; }

        public double? Chest{ get; set; }

        public double? Hips { get; set; }

        public double? Biceps { get; set; }

        public double? Tigh { get; set; }

        public double? Calf { get; set; }

        public double? Fat { get; set; }
    }
}
