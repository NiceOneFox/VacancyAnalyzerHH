using System.Text.Json.Serialization;

namespace VacancyAnalyzerHH.Core.Models;

public class AuthToken
{
    /// <summary>
    /// Токен авторизации
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Тип токена авторизации
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;
}
