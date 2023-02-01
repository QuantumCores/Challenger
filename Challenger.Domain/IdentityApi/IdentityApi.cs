using Challenger.Domain.Contracts;
using Challenger.Domain.Contracts.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using QuantumCore.Logging.Abstractions;
using QuantumCore.Network.Abstraction;
using QuantumCore.Network.Abstraction.Rest;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityApi(
            DiscoverySettings discoverySettings,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            ICorrelationIdProvider correlationIdProvider)
            : base(httpClientFactory, correlationIdProvider)
        {
            _discoverySettings = discoverySettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ApplicationUser>> SearchUsersByName(string name, Guid userId)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/Search?name={name}&userId={userId}";
            var result = await this.GetAsync<List<ApplicationUser>>(url, HttpClientType.Jwt, await GetOptions());
            return result;
        }

        public async Task<List<ApplicationUser>> GetUsers(Guid[] userIds)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/UsersInfo";
            var result = await this.PostAsync<List<ApplicationUser>>(url, userIds, HttpClientType.Jwt, await GetOptions());
            return result;
        }

        public async Task<string> UpdateUser(string avatar, Guid userId)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/Update?avatar={avatar}&userId={userId}";
            return await this.PutAsync<string>(url, null, HttpClientType.Jwt, await GetOptions());
        }

        private async Task<RequestOptions> GetOptions()
        {
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            return new RequestOptions() { ConfigureHeadres = (a, b) => b.Authorization = authorization };
        }
    }
}
