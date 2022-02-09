namespace Challenger.Domain.RankingService
{
    internal class TimeHelper
    {
        public static double CalculateTimeInMinutes(double time, string unit)
        {
            if (unit == "h")
            {
                return time * 60;
            }
            else if (unit == "m")
            {
                return time;
            }
            else if (unit == "s")
            {
                return time / 60;
            }
            else
            {
                throw new ArgumentException($"Unrecognized time unit {unit}");
            }
        }
    }
}
