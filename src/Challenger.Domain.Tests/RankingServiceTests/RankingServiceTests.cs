using AutoMapper;
using Challenger.Domain.Contracts.Identity;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.FormulaService;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Domain.Tests.RankingServiceTests
{
    public class RankingServiceTests
    {
        private IChallengeRepository _challengeRepository;
        private IGymRecordRepository _gymRecordRepository;
        private IFitRecordRepository _fitRecordRepository;
        private IMeasurementRepository _measurementRepository;
        private IFormulaService _formulaService;
        private IIdentityApi _identityApi;
        private IMapper _mapper;

        private IRankingService _sut;

        [SetUp]
        public void Setup()
        {
            _challengeRepository = Substitute.For<IChallengeRepository>();
            _gymRecordRepository = Substitute.For<IGymRecordRepository>();
            _fitRecordRepository = Substitute.For<IFitRecordRepository>();
            _measurementRepository = Substitute.For<IMeasurementRepository>();
            _identityApi = Substitute.For<IIdentityApi>();

            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<FormulaAutomapperProfile>();
            });
            _mapper = config.CreateMapper();

            var settings = new DefaultForumulaSetting[0];
            var logger = Substitute.For<ILogger<FormulaService.FormulaService>>();
            _formulaService = new FormulaService.FormulaService(settings, _challengeRepository, logger);

            _sut = new RankingService.RankingService(
                _challengeRepository,
                _gymRecordRepository,
                _fitRecordRepository,
                _measurementRepository,
                _formulaService,
                _identityApi,
                _mapper);
        }

        [Test]
        [NonParallelizable]
        public async Task TotalFitPoints_AreAddedCorrectly_NoAggregate()
        {
            // arrange
            var userGuid = Guid.NewGuid();
            var user = new User()
            {
                CorrelationId = userGuid,
            };
            var challenge = new Challenge()
            {
                Id = 1,
                AggregateFitFormula = false,
                FitFormula = "F.Duration",
                IsUsingFitDefaultFormula = false,
                StartDate = new DateTime(),
                EndDate = new DateTime().AddDays(3),
                Participants = new List<UserChallenge>
                {
                    new UserChallenge()
                    {
                         UserCorrelationId = userGuid,
                    },
                },
            };

            var fitRecords = new List<FitRecord>() {

                new FitRecord()
                {
                     RecordDate = new DateTime(),
                     Duration = 10,
                     DurationUnit = "m",
                     User = user,
                },
                new FitRecord()
                {
                    RecordDate = new DateTime().AddDays(1),
                    Duration = 20,
                    DurationUnit = "m",
                    User = user,
                },
            };

            _challengeRepository.GetWithAllData(Arg.Any<long>()).Returns(challenge);
            _challengeRepository.GetWithCustomFormulas().Returns(new List<Challenge>() { challenge });
            _fitRecordRepository.GetAllForUser(Arg.Any<long>()).Returns(fitRecords);


            _fitRecordRepository.GetAllByTimeRange(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Guid[]>())
                                .Returns(fitRecords);
            _gymRecordRepository.GetAllByTimeRange(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Guid[]>())
                                .Returns(Enumerable.Empty<GymRecord>().ToList());
            _measurementRepository.GetAllByTimeRange(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Guid[]>())
                                  .Returns(Enumerable.Empty<Measurement>().ToList());

            var appUser = new ApplicationUser
            {
                Id = userGuid,
            };
            _identityApi.GetUsers(Arg.Any<Guid[]>()).Returns(new List<ApplicationUser> { appUser });

            // act
            var result = await _sut.GetScores(1);

            // assert
            Assert.NotNull(result);
            Assert.AreEqual(20, result.UsersScores[0].TotalScore);
            Assert.AreEqual(20, result.UsersScores[0].TotalFitScore);
        }

        [Test]
        [NonParallelizable]
        public async Task TotalFitPoints_AreAddedCorrectly_WithAggregate()
        {
            // arrange
            var userGuid = Guid.NewGuid();
            var user = new User()
            {
                CorrelationId = userGuid,
            };
            var challenge = new Challenge()
            {
                Id = 1,
                AggregateFitFormula = true,
                FitFormula = "F.Duration",
                IsUsingFitDefaultFormula = false,
                StartDate = new DateTime(),
                EndDate = new DateTime().AddDays(3),
                Participants = new List<UserChallenge>
                {
                    new UserChallenge()
                    {
                         UserCorrelationId = userGuid,
                    },
                },
            };

            var fitRecords = new List<FitRecord>() {

                new FitRecord()
                {
                     RecordDate = new DateTime(),
                     Duration = 10,
                     DurationUnit = "m",
                     User = user,
                },
                new FitRecord()
                {
                    RecordDate = new DateTime().AddDays(1),
                    Duration = 20,
                    DurationUnit = "m",
                    User = user,
                },
            };

            _challengeRepository.GetWithAllData(Arg.Any<long>()).Returns(challenge);
            _challengeRepository.GetWithCustomFormulas().Returns(new List<Challenge>() { challenge });
            _fitRecordRepository.GetAllForUser(Arg.Any<long>()).Returns(fitRecords);


            _fitRecordRepository.GetAllByTimeRange(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Guid[]>())
                                .Returns(fitRecords);
            _gymRecordRepository.GetAllByTimeRange(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Guid[]>())
                                .Returns(Enumerable.Empty<GymRecord>().ToList());
            _measurementRepository.GetAllByTimeRange(Arg.Any<DateTime>(), Arg.Any<DateTime>(), Arg.Any<Guid[]>())
                                  .Returns(Enumerable.Empty<Measurement>().ToList());

            var appUser = new ApplicationUser
            {
                Id = userGuid,
            };
            _identityApi.GetUsers(Arg.Any<Guid[]>()).Returns(new List<ApplicationUser> { appUser });

            // act
            var result = await _sut.GetScores(1);

            // assert
            Assert.NotNull(result);
            Assert.AreEqual(30, result.UsersScores[0].TotalScore);
            Assert.AreEqual(30, result.UsersScores[0].TotalFitScore);
        }
    }
}