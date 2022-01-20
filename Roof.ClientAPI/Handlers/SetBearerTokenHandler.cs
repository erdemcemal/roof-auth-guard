using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Roof.ClientAPI.Models;

namespace Roof.ClientAPI.Handlers;

public class SetBearerTokenHandler : DelegatingHandler
{
    private  AuthSettings AuthSettings { get; }
    private readonly IHttpClientFactory _httpClientFactory;
    
    public SetBearerTokenHandler(IHttpClientFactory httpClientFactory, IOptions<AuthSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        AuthSettings = options.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await GetAccessToken();
        request.SetBearerToken(accessToken);
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> GetAccessToken()
    {
        var httpClient = _httpClientFactory.CreateClient();
        var discovery = await httpClient.GetDiscoveryDocumentAsync(AuthSettings.Authority);
        if (discovery.IsError)
        {
            //throw new Exception
            //logging
        }
        ClientCredentialsTokenRequest tokenRequest = new();
        tokenRequest.ClientId = AuthSettings.ClientId;
        tokenRequest.ClientSecret = AuthSettings.ClientSecrets;
        tokenRequest.Address = discovery.TokenEndpoint;

        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(tokenRequest);
        if (tokenResponse.IsError)
        {
            //throw new Exception
            //logging
        }
        return tokenResponse.AccessToken;
    }
}