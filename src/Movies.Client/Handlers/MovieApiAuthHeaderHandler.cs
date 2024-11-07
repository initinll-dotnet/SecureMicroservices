using Movies.Client.Services;
using Refit;

using System.Net.Http.Headers;

namespace Movies.Client.Handlers;

public class MovieApiAuthHeaderHandler : DelegatingHandler
{
    private readonly IAuthTokenProvider authTokenProvider;

    public MovieApiAuthHeaderHandler(IAuthTokenProvider authTokenProvider)
    {
        this.authTokenProvider = authTokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var contentKey = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("client_id", "movieClient"),
            new KeyValuePair<string, string>("client_secret", "secret"),
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("scope", "movieAPI")
        };

        FormUrlEncodedContent content = new FormUrlEncodedContent(contentKey);

        TokenResponse tokenResponse;
        try
        {
            tokenResponse = await authTokenProvider.GetToken(content);
        }
        catch (ApiException ex)
        {
            // Extract the details of the error
            var errors = await ex.GetContentAsAsync<Dictionary<string, string>>();
            // Combine the errors into a string
            var message = string.Join("; ", errors!.Values);
            // Throw a normal exception
            throw new Exception(message);
        }

        var token = tokenResponse.access_token;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
