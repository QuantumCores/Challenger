using AutoMapper;
using Challenger.Domain.DbModels;

namespace Challenger.Domain.Dtos
{
    public class DtoAutomapperProfile : Profile
    {
        public DtoAutomapperProfile()
        {
            CreateMap<FitRecordDto, FitRecord>();
            CreateMap<GymRecordDto, GymRecord>();
            CreateMap<MeasurementDto, Measurement>();
        }
    }
}
