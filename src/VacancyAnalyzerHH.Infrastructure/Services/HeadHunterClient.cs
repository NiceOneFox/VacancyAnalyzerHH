using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using VacancyAnalyzerHH.Contracts.Interfaces;
using VacancyAnalyzerHH.Core.Models;

namespace VacancyAnalyzerHH.Infrastructure.Services;

/// <summary>
/// Клиент для взаимодействия с API
/// </summary>
public class HeadHunterClient : IHeadHunterClient
{
    private static readonly string baseUrl = "https://api.hh.ru";

    private readonly ILogger _logger;
    private readonly IMemoryCache _memoryCache;

    public HeadHunterClient(
        ILogger<HeadHunterClient> logger,
        IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    /// <inheritdoc/>
    public async Task<string> AuthorizeAsync(AuthApplicationModel authApplicationModel, CancellationToken cancellationToken)
    {
        try
        {
            _memoryCache.TryGetValue(authApplicationModel.Code, out string? cachedAuthToken);

            if (!string.IsNullOrWhiteSpace(cachedAuthToken)) return cachedAuthToken;

            var tokenRequest = await baseUrl
                .AppendPathSegment("token")
                .PostUrlEncodedAsync(new
                {
                    grant_type = authApplicationModel.GrantType,
                    client_id = authApplicationModel.ClientId,
                    client_secret = authApplicationModel.ClientSecret,
                    code = authApplicationModel.Code,
                    redirect_uri = authApplicationModel.RedirectUri
                }, 
                cancellationToken: cancellationToken);

            var token = await tokenRequest.GetJsonAsync<AuthToken>();

            _memoryCache.Set(authApplicationModel.Code, token.AccessToken);

            return token.AccessToken;

        }
        catch (FlurlHttpException ex)
        {
            // токен можно запрашивать не чаще, чем один раз в 5 минут.
            _logger.LogError(ex, "Error returned from {Url}: {Message}", ex.Call.Request.Url, ex.Message);
            return string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);
            return string.Empty;
        }
    }

    /// <inheritdoc/>
    public async Task<string> GetVacancyAsync(int vacancyId, string accessToken, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException("Необходимо указать токен авторизации");

            var resultOneVacancy = await baseUrl
                .AppendPathSegment("vacancies")
                .AppendPathSegment(vacancyId)
                .WithHeader("User-Agent", "VacancyAnalyzerHH/1.0 (kulakovskij.es@gmail.com)") // TODO appsettings
                .WithOAuthBearerToken(accessToken)
                .GetStringAsync(cancellationToken: cancellationToken);

            return resultOneVacancy;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);
            return string.Empty;
        }
    }
}
