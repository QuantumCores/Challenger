using AutoMapper;
using Challenger.Domain.Contracts.Identity;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Challenger.Domain.ChallengeService
{
    public class ChallengeService : IChallengeService
    {
        private readonly ChallengeSettings _challengeSettings;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserChallengeRepository _userChallengeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFormulaService _formulaService;
        private readonly IIdentityApi _identityApi;
        private readonly IMapper _mapper;

        public ChallengeService(
            ChallengeSettings challengeSettings,
            IChallengeRepository challengeRepository,
            IUserChallengeRepository userChallengeRepository,
            IUserRepository userRepository,
            IFormulaService formulaService,
            IIdentityApi identityApi,
            IMapper mapper)
        {

            _challengeSettings = challengeSettings;
            _challengeRepository = challengeRepository;
            _userChallengeRepository = userChallengeRepository;
            _userRepository = userRepository;
            _formulaService = formulaService;
            _identityApi = identityApi;
            _mapper = mapper;
        }

        public async Task<ChallengeDisplayDto[]> GetForUser(Guid userId)
        {
            var all = await _userChallengeRepository.GetAllForUser(userId);
            var mapped = _mapper.Map<ChallengeDisplayDto[]>(all.Select(x => x.Challenge));
            var allParticipants = mapped.SelectMany(x => x.Participants).ToList();
            var users = allParticipants.Select(x => x.Id).ToList();
            users.AddRange(mapped.Select(x => x.Creator.Id));
            users = users.Distinct().ToList();
            var usersInfo = await _identityApi.GetUsers(users.ToArray());
            var usersDict = usersInfo.ToDictionary(x => x.Id);

            foreach (var participant in allParticipants)
            {
                participant.Avatar = usersDict[participant.Id].Avatar;
                participant.UserName = usersDict[participant.Id].UserName;
            }

            foreach (var challenge in mapped)
            {
                challenge.Creator.Avatar = usersDict[challenge.Creator.Id].Avatar;
                challenge.Creator.UserName = usersDict[challenge.Creator.Id].UserName;
            }

            return mapped;
        }

        public async Task<Challenge> CreateChallenge(ChallengeDto record)
        {
            var userCreatedChallenges = await _challengeRepository.GetAllForUser(record.CreatorId);
            var userParticipatedChallenges = await _userChallengeRepository.GetAllForUser(record.CreatorId);

            if (_challengeSettings.MaxChallengesCreatorForRegular <= userCreatedChallenges.Count ||
                _challengeSettings.MaxChallengesAsParticipantForRegular <= userParticipatedChallenges.Count)
            {
                throw new System.Exception();
            }

            if (_challengeSettings.MaxParticipantsForRegular < record.Participants.Count)
            {
                throw new System.Exception();
            }

            var fit = _formulaService.ValidateFitFormula(record.FitFormula);
            var gym = _formulaService.ValidateGymFormula(record.GymFormula);
            var mes = _formulaService.ValidateMeasurementFormula(record.MeasurementFormula);

            if (!(fit.IsValid && gym.IsValid && mes.IsValid))
            {
                throw new System.Exception("One of the formulas is invalid");
            }

            var entity = _mapper.Map<Challenge>(record);
            var creator = await _userRepository.GetByCorrelationId(record.CreatorId);
            entity.UserId = creator.Id;

            var participants = await _userRepository.GetManyByCorrelationId(record.Participants.Select(x => x.UserCorrelationId).ToArray());
            var participantsDict = participants.ToDictionary(x => x.CorrelationId);
            foreach (var participant in entity.Participants)
            {
                participant.UserId = participantsDict[participant.UserCorrelationId].Id;
            }
            entity.Participants.Add(new UserChallenge { UserId = creator.Id, UserCorrelationId = creator.CorrelationId });

            _challengeRepository.Add(entity);
            await _challengeRepository.SaveChanges();

            return entity;
        }

        public async Task<ChallengeDisplayDto[]> GetByName(Guid userId, string name)
        {
            var found = await _challengeRepository.GetByName(userId,  name);            
            var mapped = _mapper.Map<ChallengeDisplayDto[]>(found);

            var users = mapped.SelectMany(x => x.Participants.Select(x => x.Id)).ToArray();
            var usersInfo = await _identityApi.GetUsers(users.ToArray());
            var usersDict = usersInfo.ToDictionary(x => x.Id);

            var allParticipants = mapped.SelectMany(x => x.Participants).ToList();
            foreach (var participant in allParticipants)
            {
                participant.Avatar = usersDict[participant.Id].Avatar;
                participant.UserName = usersDict[participant.Id].UserName;
            }

            foreach (var challenge in mapped)
            {
                challenge.Creator.Avatar = usersDict[challenge.Creator.Id].Avatar;
                challenge.Creator.UserName = usersDict[challenge.Creator.Id].UserName;
            }

            return mapped;
        }
    }
}
