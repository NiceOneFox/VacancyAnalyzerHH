namespace VacancyAnalyzerHH.Core.Repositories;

public interface IVacancyRepository
{
    /// <summary>
    /// Вставка одной вакансии
    /// </summary>
    /// <param name="vacancy">json вакансии</param>
    /// <returns></returns>
    Task InsertOneAsync(string vacancy);
}
