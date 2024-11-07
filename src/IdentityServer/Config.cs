using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;

using IdentityModel;

using System.Security.Claims;

namespace IdentityServer;

public class Config
{
    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "movieClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "movieAPI" }
            },
            new Client
            {
                ClientId = "movies_mvc_client",
                ClientName = "Movies MVC Web App",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RequirePkce = false,
                AllowRememberConsent = false,
                RedirectUris =
                [
                    "http://localhost:5002/signin-oidc"
                ],
                PostLogoutRedirectUris =
                [
                    "http://localhost:5002/signout-callback-oidc"
                ],
                ClientSecrets =
                [
                    new Secret("secret".Sha256())
                ],
                AllowedScopes =
                [
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    "movieAPI",
                    "roles"
                ]
            }
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
       [
            new ApiScope(name:"movieAPI", displayName:"Movie API")
       ];

    public static IEnumerable<ApiResource> ApiResources =>
      [
          //new ApiResource(name:"movieAPI", displayName:"Movie API")
      ];

    public static IEnumerable<IdentityResource> IdentityResources =>
      [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResources.Email(),
            new IdentityResource(
                name:"roles",
                displayName:"Your role(s)",
                userClaims:["role"])
      ];

    public static List<TestUser> TestUsers =>
        [
            new TestUser
            {
                SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                Username = "mehmet",
                Password = "swn",
                Claims =
                [
                    new Claim(type:JwtClaimTypes.GivenName, value:"mehmet"),
                    new Claim(type:JwtClaimTypes.FamilyName, value:"ozkaya")
                ]
            }
        ];
}
