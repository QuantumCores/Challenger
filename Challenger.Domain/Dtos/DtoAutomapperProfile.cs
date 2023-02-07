using AutoMapper;
using Challenger.Domain.Contracts.Identity;
using Challenger.Domain.DbModels;
using Challenger.Domain.RankingService;
using System.Collections.Generic;

namespace Challenger.Domain.Dtos
{
    public class DtoAutomapperProfile : Profile
    {
        public DtoAutomapperProfile()
        {
            CreateMap<ApplicationUser, User>()
                .ForMember(x => x.CorrelationId, opt => opt.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<ApplicationUser, UserChallenge>()
                .ForMember(x => x.UserCorrelationId, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ChallengeDisplayDto, Challenge>()
                .ForMember(x => x.User, opt => opt.MapFrom(y => y.Creator))
                .ReverseMap();

            CreateMap<ChallengeDto, Challenge>()
                .ReverseMap();

            CreateMap<UserChallengeDto, UserChallenge>()
                .ReverseMap();

            CreateMap<FitRecordDto, FitRecord>()
                .ReverseMap();

            CreateMap<GymRecordDto, GymRecord>()
                .ReverseMap();

            CreateMap<MeasurementDto, Measurement>()
                .ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(x => x.CorrelationId, opt => opt.MapFrom(y => y.Id))
                .ForMember(x => x.Height, opt => opt.MapFrom(y => y.Height))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(y => y.DateOfBirth))
                .ForMember(x => x.Sex, opt => opt.MapFrom(y => y.Sex))
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.CorrelationId))
                .ForMember(x => x.Height, opt => opt.MapFrom(y => y.Height))
                .ForMember(x => x.DateOfBirth, opt => opt.MapFrom(y => y.DateOfBirth))
                .ForMember(x => x.Sex, opt => opt.MapFrom(y => y.Sex));

            CreateMap<ProductDto, Product>()
                .ReverseMap();

            CreateMap<DishDto, Dish>()
                .ReverseMap();

            CreateMap<IngridientDto, Ingridient>()
                .ReverseMap()
                .ForMember(x => x.ProductName, y => y.MapFrom(src => src.Product.Name)); ;

            CreateMap<MealRecordDto, MealRecord>()
                .ReverseMap();

            CreateMap<FastRecordDto, FastRecord>()
                .ReverseMap();

            CreateMap<MealDishDto, MealDish>()
                .ForMember(x => x.Dish, y => y.Ignore())
                .ReverseMap()
                .ForMember(x => x.DishName, y => y.MapFrom(src => src.Dish.Name));

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
