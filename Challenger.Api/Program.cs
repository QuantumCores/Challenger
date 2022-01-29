using Autofac;
using Autofac.Extensions.DependencyInjection;
using Challenger.Domain.Contracts;
using Challenger.Domain.Dtos;
using Challenger.Domain.RankingService;
using Challenger.Infrastructure;
using Challenger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterType<FitRecordRepository>().As<IFitRecordRepository>();
    builder.RegisterType<GymRecordRepository>().As<IGymRecordRepository>();
    builder.RegisterType<MeasurementRepository>().As<IMeasurementRepository>();
    builder.RegisterType<RankingService>().As<IRankingService>();
});

builder.Services.AddDbContext<ChallengerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

builder.Services.AddAutoMapper(
    typeof(Program).Assembly,
    typeof(FitRecordDto).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "testujemyCors",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .WithHeaders("content-type")
                          .AllowCredentials();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("testujemyCors");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
