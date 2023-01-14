using AutoMapper;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
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
        private readonly IMapper _mapper;

        public ChallengeService(
            ChallengeSettings challengeSettings,
            IChallengeRepository challengeRepository,
            IUserChallengeRepository userChallengeRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {

            _challengeSettings = challengeSettings;
            _challengeRepository = challengeRepository;
            _userChallengeRepository = userChallengeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
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
    }
}
