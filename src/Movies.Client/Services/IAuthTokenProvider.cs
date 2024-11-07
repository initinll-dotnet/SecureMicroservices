using Refit;

namespace Movies.Client.Services;

public interface IAuthTokenProvider
{
    [Headers("Content-Type: application/x-www-form-urlencoded")]
    [Post("/connect/token")]
    Task<TokenResponse> GetToken([Body(BodySerializationMethod.UrlEncoded)] FormUrlEncodedContent content);
}

public record TokenResponse
{
    public string access_token { get; init; }
    public int expires_in { get; init; }
    public string token_type { get; init; }
    public string scope { get; init; }
}
