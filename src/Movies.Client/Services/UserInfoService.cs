using IdentityModel.Client;

using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Movies.Client.Models;

namespace Movies.Client.Services;

public class UserInfoService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserInfoService(
        IHttpClientFactory httpClientFactory, 
        IHttpContextAccessor httpContextAccessor)
    {
        this.httpClientFactory = httpClientFactory;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserInfoViewModel> GetUserInfo()
    {
        var idpClient = httpClientFactory.CreateClient("IDPClient");

        var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

        if (metaDataResponse.IsError)
        {
            throw new HttpRequestException("Something went wrong while requesting the access token");
        }

        var accessToken = await httpContextAccessor
            .HttpContext!.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var userInfoResponse = await idpClient.GetUserInfoAsync(
           new UserInfoRequest
           {
               Address = metaDataResponse.UserInfoEndpoint,
               Token = accessToken
           });

        if (userInfoResponse.IsError)
        {
            throw new HttpRequestException("Something went wrong while getting user info");
        }

        var userInfoDictionary = new Dictionary<string, string>();

        foreach (var claim in userInfoResponse.Claims)
        {
            userInfoDictionary.Add(claim.Type, claim.Value);
        }

        return new UserInfoViewModel(userInfoDictionary);
    }
}
