using AutoMapper;
using Challenger.Domain.DbModels;
using Challenger.Domain.RankingService;

namespace Challenger.Domain.FormulaService
{
    public class FormulaAutomapperProfile : Profile
    {
        public FormulaAutomapperProfile()
        {
            ValueTransformers.Add<int?>(val => val ?? 0);
            ValueTransformers.Add<double?>(val => val ?? 0);

            CreateMap<FitRecord, FitFormulaRecord>()
                .ForMember(x => x.Calories, opt => opt.MapFrom(y => y.BurntCalories))
                .ForMember(x => x.Duration, opt => opt.MapFrom(y => !y.Duration.HasValue ? 0 : TimeHelper.CalculateTimeInMinutes(y.Duration.Value, y.DurationUnit)));

            CreateMap<GymRecord, GymFormulaRecord>()
                .ForMember(x => x.Muscle, opt => opt.MapFrom(y => y.MuscleGroup));

            CreateMap<Measurement, MeasurementFormulaRecord>();
        }
    }
}
