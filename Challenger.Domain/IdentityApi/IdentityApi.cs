using Challenger.Domain.Contracts;
using Challenger.Domain.Contracts.Identity;
using QuantumCore.Logging.Abstractions;
using QuantumCore.Network.Rest;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Challenger.Domain.IdentityApi
{
    public class IdentityApi : RestApi, IIdentityApi
    {
        private DiscoverySettings _discoverySettings;

        public IdentityApi(
            DiscoverySettings discoverySettings,
            IHttpClientFactory httpClientFactory, 
            ICorrelationIdProvider correlationIdProvider) : base(httpClientFactory, correlationIdProvider)
        {
            _discoverySettings = discoverySettings;
        }

        public Task<List<IdentityUser>> SearchUsersByName(string name)
        {
            var url = $"{_discoverySettings.IdentityUrl}/User/Search?name={name}";
            return this.GetAsync<List<IdentityUser>>(url, QuantumCore.Network.Abstraction.HttpClientType.Default);
        }
    }
}
