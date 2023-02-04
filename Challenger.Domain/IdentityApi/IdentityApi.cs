using Challenger.Domain.Contracts;
using Challenger.Domain.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using QuantumCore.Logging.Abstractions;
using QuantumCore.Network.Abstraction;
using QuantumCore.Network.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenger.Domain.IdentityApi
{
    public class IdentityApi : RestApi, IIdentityApi
    {
        private readonly DiscoverySettings _discoverySettings;

        public IdentityApi(
            DiscoverySettings discoverySettings,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            ICorrelationIdProvider correlationIdProvider)
            : base(httpContextAccessor, httpClientFactory, correlationIdProvider)
        {
            _discoverySettings = discoverySettings;
        }

        public async Task<List<ApplicationUser>> SearchUsersByName(string name, Guid userId)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/Search?name={name}&userId={userId}";
            var result = await this.GetAsync<List<ApplicationUser>>(url, HttpClientType.Jwt);
            return result;
        }

        public async Task<List<ApplicationUser>> GetUsers(Guid[] userIds)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/UsersInfo";
            var result = await this.PostAsync<List<ApplicationUser>>(url, userIds, HttpClientType.Jwt);
            return result;
        }

        public async Task<string> UpdateUser(string avatar, Guid userId)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/Update?avatar={avatar}&userId={userId}";
            return await this.PutAsync<string>(url, null, HttpClientType.Jwt);
        }
    }
}
