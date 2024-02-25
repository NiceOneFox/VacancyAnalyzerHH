namespace VacancyAnalyzerHH.Core.Models;

/// <summary>
/// Модель аутентификации приложения
/// </summary>
/// <param name="GrantType"></param>
/// <param name="ClientId"></param>
/// <param name="ClientSecret"></param>
/// <param name="Code"></param>
/// <param name="RedirectUri"></param>
public record AuthApplicationModel(
    string GrantType,
    string ClientId,
    string ClientSecret,
    string Code,
    string RedirectUri);