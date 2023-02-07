using AutoMapper;
using Challenger.Domain.Contracts.Identity;
using Challenger.Domain.Contracts.Repositories;
using Challenger.Domain.Contracts.Services;
using Challenger.Domain.DbModels;
using Challenger.Domain.Dtos;
using System;
using System.Collections.Generic;
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

        public async Task<ChallengeDto> GetChallenge(long id)
        {
            var entity = await _challengeRepository.GetWithAllData(id);
            var allParticipants = entity.Participants.Select(x => x.UserCorrelationId).ToArray();
            var users = await _identityApi.GetUsers(allParticipants);
            var result = _mapper.Map<ChallengeDto>(entity);
            var usersDict = users.ToDictionary(x => x.Id);

            foreach (var participant in result.Participants)
            {
                participant.Avatar = usersDict[participant.Id].Avatar;
                participant.UserName = usersDict[participant.Id].UserName;
            }

            return result;
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

            var participants = await _userRepository.GetManyByCorrelationId(record.Participants.Select(x => x.Id).ToArray());
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
            var found = await _challengeRepository.GetByName(userId, name);
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

        public async Task<bool> JoinChallenge(Guid userId, int challengeId)
        {
            var challenge = await _challengeRepository.GetWithAllData(challengeId);
            if (_challengeSettings.MaxParticipantsForRegular <= challenge.Participants.Count)
            {
                throw new System.Exception();
            }

            var count = await _userChallengeRepository.GetCountForUser(userId);
            if (_challengeSettings.MaxChallengesAsParticipantForRegular <= count)
            {
                throw new System.Exception();
            }

            var id = await _userRepository.GetIdByCorrelationId(userId.ToString());
            challenge.Participants.Add(new UserChallenge { ChallengeId = challenge.Id, UserCorrelationId = userId, UserId = id });
            await _challengeRepository.Update(challenge);
            await _challengeRepository.SaveChanges();

            return true;
        }

        public async Task<ChallengeDto> UpdateChallenge(ChallengeDto record)
        {
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

            var entity = await _challengeRepository.Get(record.Id);
            entity = _mapper.Map(record, entity);
            var creator = await _userRepository.GetByCorrelationIdNoTracking(record.CreatorId);
            entity.UserId = creator.Id;
            entity.Participants = null;

            await _challengeRepository.Update(entity);
            await _challengeRepository.SaveChanges();

            var original = await _userChallengeRepository.GetForChallenge(entity.Id);

            var existing = original.ToDictionary(x => x.UserCorrelationId);
            var update = record.Participants.ToDictionary(x => x.Id, x => new UserChallenge { UserCorrelationId = x.Id });
            var partition = Partition(existing, update);

            var rmvList = original.Where(x => x.UserCorrelationId != creator.CorrelationId &&
                                              partition.toRmv.ContainsKey(x.UserCorrelationId))
                                             .ToList();

            foreach (var rmv in rmvList)
            {
                _userChallengeRepository.Remove(rmv);
            }

            var addList = await _userRepository.GetManyByCorrelationId(partition.toAdd.Select(x => x.Key).ToArray());
            var addListDict = addList.ToDictionary(x => x.CorrelationId);
            foreach (var add in addList)
            {
                _userChallengeRepository.Add(new UserChallenge
                {
                    ChallengeId = entity.Id,
                    UserId = addListDict[add.CorrelationId].Id,
                    UserCorrelationId = add.CorrelationId
                });
            }
            
            await _userChallengeRepository.SaveChanges();

            var mapped = _mapper.Map<ChallengeDto>(entity);
            mapped.Participants = record.Participants;

            return mapped;
        }

        private (Dictionary<TKey, TVal> toAdd, Dictionary<TKey, TVal> toRmv, Dictionary<TKey, TVal> toUpd)
            Partition<TKey, TVal>(Dictionary<TKey, TVal> existing, Dictionary<TKey, TVal> update)
        {
            var toAdd = new Dictionary<TKey, TVal>();
            var toRmv = new Dictionary<TKey, TVal>();
            var toUpd = new Dictionary<TKey, TVal>();

            foreach (var key in update.Keys)
            {
                if (existing.ContainsKey(key))
                {
                    toUpd.Add(key, update[key]);
                }
                else
                {
                    toAdd.Add(key, update[key]);
                }
            }

            foreach (var key in existing.Keys)
            {
                if (!update.ContainsKey(key))
                {
                    toRmv.Add(key, existing[key]);
                }
            }

            return (toAdd, toRmv, toUpd);
        }
    }
}
