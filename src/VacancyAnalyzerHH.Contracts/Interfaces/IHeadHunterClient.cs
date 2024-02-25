using VacancyAnalyzerHH.Core.Models;

namespace VacancyAnalyzerHH.Contracts.Interfaces;

public interface IHeadHunterClient
{
    /// <summary>
    /// Авторизация приложения
    /// </summary>
    /// <param name="authApplicationModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> AuthorizeAsync(AuthApplicationModel authApplicationModel, CancellationToken cancellationToken);

    /// <summary>
    /// Получение вакансии по идентификатору
    /// </summary>
    /// <param name="vacancyId">идентификатор вакансии</param>
    /// <param name="accessToken">токен авторизации</param>
    /// <param name="cancellationToken"></param>
    /// <returns>информация о вакансии</returns>
    Task<string> GetVacancyAsync(int vacancyId, string accessToken, CancellationToken cancellationToken);
}
