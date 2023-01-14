using AutoMapper;
using Challenger.Domain.DbModels;

namespace Challenger.Domain.FormulaService
{
    public class FormulaAutomapperProfile : Profile
    {
        public FormulaAutomapperProfile()
        {
            ValueTransformers.Add<int?>(val => val ?? 0);
            ValueTransformers.Add<double?>(val => val ?? 0);

            CreateMap<FitRecord, FitFormulaRecord>();
                

            CreateMap<GymRecord, GymFormulaRecord>();
                

            CreateMap<Measurement, MeasurementFormulaRecord>();                
        }
    }
}
