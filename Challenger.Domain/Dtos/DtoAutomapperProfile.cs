using AutoMapper;
using Challenger.Domain.DbModels;
using Challenger.Domain.RankingService;

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

            CreateMap<UserDto, User>()
                .ReverseMap();

            CreateMap<KeyValuePair<string, double>, KeyValuePair<string, string>>()
                .ConstructUsing(x => new KeyValuePair<string, string>(x.Key,x.Value.ToString()));

            CreateMap<RankingSettings, RulesDto>();
        }
    }
}
