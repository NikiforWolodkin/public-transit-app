using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AuthenticationServer
{
    public static class Config
    {
        public static List<TestUser> Users => new List<TestUser>();

        public static IEnumerable<IdentityResource> IdentityResources => 
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes => 
            new[]
            {
                new ApiScope("publictransitapi.scope")
            };

        public static IEnumerable<ApiResource> ApiResources => 
            new[]
            {
                new ApiResource("publictransitapi")
                {
                    Scopes = new List<string> {"publictransitapi.scope"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                    AllowedScopes = {"publictransitapi.scope"}
                },
            };
    }
}