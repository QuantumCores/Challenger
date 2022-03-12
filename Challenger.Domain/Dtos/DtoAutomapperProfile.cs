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

            CreateMap<ProductDto, Product>()
                .ReverseMap();

            CreateMap<DishDto, Dish>()
                .ReverseMap();

            CreateMap<IngridientDto, Ingridient>()
                .ReverseMap();

            CreateMap<MealRecordDto, MealRecord>()
                .ReverseMap();

            CreateMap<MealProductDto, MealProduct>()
                .ForMember(x => x.Product, y => y.Ignore())
                .ReverseMap()
                .ForMember(x => x.ProductName, y => y.MapFrom(src => src.Product.Name));

            CreateMap<DiaryRecordDto, DiaryRecord>()
                .ForMember(x => x.DiaryDate, y => y.MapFrom(src => src.DiaryDate.Date))
                .ReverseMap();

            CreateMap<KeyValuePair<string, double>, KeyValuePair<string, string>>()
                .ConstructUsing(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString()));

            CreateMap<RankingSettings, RulesDto>();
        }
    }
}
