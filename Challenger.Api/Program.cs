using Autofac;
using Autofac.Extensions.DependencyInjection;
using Challenger.Domain.Contracts;
using Challenger.Domain.Dtos;
using Challenger.Domain.RankingService;
using Challenger.Infrastructure;
using Challenger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Challenger.Domain.Account;
using Challenger.Domain.Contracts.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var rankingSettings = new RankingSettings();
builder.Configuration.GetSection(nameof(RankingSettings)).Bind(rankingSettings);

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterInstance(rankingSettings);
    builder.RegisterType<FitRecordRepository>().As<IFitRecordRepository>();
    builder.RegisterType<GymRecordRepository>().As<IGymRecordRepository>();
    builder.RegisterType<UserRepository>().As<IUserRepository>();
    builder.RegisterType<MeasurementRepository>().As<IMeasurementRepository>();

    builder.RegisterType<ProductRepository>().As<IProductRepository>();
    builder.RegisterType<DishRepository>().As<IDishRepository>();
    builder.RegisterType<IngridientRepository>().As<IIngridientRepository>();
    builder.RegisterType<DiaryRecordRepository>().As<IDiaryRecordRepository>();
    builder.RegisterType<MealRecordRepository>().As<IMealRecordRepository>();
    builder.RegisterType<MealProductRepository>().As<IMealProductRepository>();

    builder.RegisterType<FastRecordRepository>().As<IFastRecordRepository>();
    builder.RegisterType<MealDishRepository>().As<IMealDishRepository>();

    builder.RegisterType<RankingService>().As<IRankingService>();
    builder.RegisterType<AccountService>().As<IAccountService>();
    builder.RegisterType<JwtService>().As<IJwtService>();

});

builder.Services.AddDbContext<ChallengerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

builder.Services.AddDbContext<ChallengerFoodContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodConnection")));

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<IdentityContext>();


var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(x =>
{
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
        ValidAudience = jwtSettings.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.GetSection("securityKey").Value)),
    };
});


builder.Services.AddAutoMapper(
    typeof(Program).Assembly,
    typeof(FitRecordDto).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAnyOrigin",
                      builder =>
                      {
                          builder//.WithOrigins("http://54.37.137.86", "https://54.37.137.86", "http://localhost:4200")
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                          //.WithHeaders("content-type")
                          //.AllowCredentials();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
