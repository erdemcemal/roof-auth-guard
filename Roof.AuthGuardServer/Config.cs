using System.Collections.Generic;
using IdentityServer4.Models;

namespace Roof.AuthGuardServer
{
    internal static class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("roof.api", "Roof API")
                {
                    Scopes = new List<string>
                        { "roof.api_read", "roof.api_write", "roof.api_update", "roof.api_delete" }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("roof.api_read", "Read Roof API Data"),
                new ApiScope("roof.api_write", "Write Roof API Data"),
                new ApiScope("roof.api_update", "Update Roof API Data"),
                new ApiScope("roof.api_delete", "Delete Roof API Data")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "roof.client",
                    ClientName = "Razor Employee Web App",
                    ClientSecrets = new[] { new Secret("adb876e3-344b-4baf-88c3-58eb701870de".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials, // Create token with this allowed grant types. 
                    AllowedScopes = { "roof.api_read" }
                }
            };
        }
    }
}