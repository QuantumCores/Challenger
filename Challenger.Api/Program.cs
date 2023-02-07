using Autofac;
using Autofac.Extensions.DependencyInjection;
using Challenger.Domain.ChallengeService;
using Challenger.Domain.Contracts;
using Challenger.Domain.Contracts.Identity;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.Dtos;
using Challenger.Domain.FormulaService;
using Challenger.Domain.IdentityApi;
using Challenger.Domain.RankingService;
using Challenger.Infrastructure;
using Challenger.Infrastructure.Repositories;
using Heimdal.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuantumCore.Logging.Abstractions;
using QuantumCore.Logging.Api;
using System.Collections.Generic;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add and configure CHALLENGER Env settings
builder.Configuration.AddEnvironmentVariables(prefix: "CHALLENGER_");

var rankingSettings = new RankingSettings();
builder.Configuration.GetSection(nameof(RankingSettings)).Bind(rankingSettings);

var challengeSettings = new ChallengeSettings();
builder.Configuration.GetSection(nameof(ChallengeSettings)).Bind(challengeSettings);

var discoverySettings = new Discovery();
builder.Configuration.GetSection(nameof(Discovery)).Bind(discoverySettings);

var challengeDefaultFormulas = new List<DefaultForumulaSetting>();
builder.Configuration.GetSection("ChallengeDefaultFormulas").Bind(challengeDefaultFormulas);

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterInstance(rankingSettings);
    builder.RegisterInstance(challengeSettings);
    builder.RegisterInstance(discoverySettings);
    builder.RegisterInstance(challengeDefaultFormulas.ToArray());

    builder.RegisterType<IdentityApi>().As<IIdentityApi>();
    builder.RegisterType<CorrelationIdProvider>().As<ICorrelationIdProvider>();

    builder.RegisterType<FitRecordRepository>().As<IFitRecordRepository>();
    builder.RegisterType<GymRecordRepository>().As<IGymRecordRepository>();
    builder.RegisterType<UserRepository>().As<IUserRepository>();
    builder.RegisterType<MeasurementRepository>().As<IMeasurementRepository>();
    builder.RegisterType<ChallengeRepository>().As<IChallengeRepository>();
    builder.RegisterType<UserChallengeRepository>().As<IUserChallengeRepository>();

    builder.RegisterType<ProductRepository>().As<IProductRepository>();
    builder.RegisterType<DishRepository>().As<IDishRepository>();
    builder.RegisterType<IngridientRepository>().As<IIngridientRepository>();
    builder.RegisterType<DiaryRecordRepository>().As<IDiaryRecordRepository>();
    builder.RegisterType<MealRecordRepository>().As<IMealRecordRepository>();
    builder.RegisterType<MealProductRepository>().As<IMealProductRepository>();
    builder.RegisterType<FastRecordRepository>().As<IFastRecordRepository>();
    builder.RegisterType<MealDishRepository>().As<IMealDishRepository>();

    builder.RegisterType<RankingService>().As<IRankingService>();
    builder.RegisterType<FormulaService>().As<IFormulaService>();
    builder.RegisterType<ChallengeService>().As<IChallengeService>();
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTokenProvider(builder.Configuration);

builder.Services.AddDbContext<ChallengerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"))
           .EnableSensitiveDataLogging());

builder.Services.AddDbContext<ChallengerFoodContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodConnection")));

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = discoverySettings.IdentityApi;
        opt.Audience = "challenger";
    });


builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(UserDto).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAnyOrigin",
                      builder =>
                      {
                          builder.WithOrigins(
                              discoverySettings.ChallengerApi,
                              discoverySettings.ChallengerWeb,
                              discoverySettings.IdentityApi)
                          //.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                          //.WithHeaders("content-type")
                          //.AllowCredentials();
                      });
});

builder.Services.AddControllers()
                //.AddNewtonsoftJson()
                .AddJsonOptions(options =>
                                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

// APP MIDDLEWARES HERE
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
