using Challenger.Domain.Contracts;
using Challenger.Domain.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using QuantumCore.Logging.Abstractions;
using QuantumCore.Network.Abstraction.Rest;
using QuantumCore.Network.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Challenger.Domain.IdentityApi
{
    public class IdentityApi : RestApi, IIdentityApi
    {
        private DiscoverySettings _discoverySettings;
        private IHttpContextAccessor _httpContextAccesor;

        public IdentityApi(
            DiscoverySettings discoverySettings,
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccesor,
            ICorrelationIdProvider correlationIdProvider)
            : base(httpClientFactory, correlationIdProvider)
        {
            _discoverySettings = discoverySettings;
            _httpContextAccesor = httpContextAccesor;
        }

        public Task<List<IdentityUser>> SearchUsersByName(string name, Guid userId)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/Search?name={name}&userId={userId}";
            return this.GetAsync<List<IdentityUser>>(url, QuantumCore.Network.Abstraction.HttpClientType.Default);
        }
    }
}
