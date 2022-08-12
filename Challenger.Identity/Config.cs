// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Challenger.Identity
{
    public static class Config
    {
        //public static IEnumerable<IdentityResource> IdentityResources =>
        //    new IdentityResource[]
        //    {
        //        new IdentityResources.OpenId()
        //    };

        //public static IEnumerable<ApiScope> ApiScopes =>
        //    new ApiScope[]
        //    { };

        //public static IEnumerable<Client> Clients =>
        //    new Client[]
        //    { };


        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("challenger", "Challanger main API")
                {
                    Scopes = { "challenger" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("challenger", "Challanger main API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new ArdIdentityResource(),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                //new Client
                //{
                //    ClientId = "client",

                //    // no interactive user, use the clientid/secret for authentication
                //    AllowedGrantTypes = GrantTypes.ClientCredentials,

                //    // secret for authentication
                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },

                //    // scopes that client has access to
                //    AllowedScopes = { "api1" }
                //},
                // machine to machine client (from quickstart 1)
                new Client
                {
                    ClientId = "challenger",
                    ClientName = "Challenger main API",
                    ClientSecrets = { new Secret("challenger.secret".Sha256()) },
                    RequirePkce = false,
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    // scopes that client has access to
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "challenger"
                    },
                },
                // interactive ASP.NET Core MVC client
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    RequireConsent = true,

                    AllowedGrantTypes = GrantTypes.Code,
        
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:6002/signin-oidc" },
        
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:6002/signout-callback-oidc" },

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "ard",
                        "challenger",
                    }
                },
                new Client
                {
                    ClientName = "Challenger_web",
                    ClientId = "challenger_web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>{ "http://localhost:4200" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "challenger"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
                }
            };
    }
}