using AutoMapper;
using Challenger.Domain.DbModels;

namespace Challenger.Domain.Dtos
{
    public class DtoAutomapperProfile : Profile
    {
        public DtoAutomapperProfile()
        {
            CreateMap<FitRecordDto, FitRecord>()
                .ReverseMap();

            CreateMap<GymRecordDto, GymRecord>()
                .ReverseMap();

            CreateMap<MeasurementDto, Measurement>()
                .ReverseMap();
        }
    }
}
