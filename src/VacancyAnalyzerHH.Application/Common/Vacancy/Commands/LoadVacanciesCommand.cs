using MediatR;
using Microsoft.Extensions.Logging;
using VacancyAnalyzerHH.Contracts.Interfaces;
using VacancyAnalyzerHH.Core.Models;
using VacancyAnalyzerHH.Core.Repositories;

namespace VacancyAnalyzerHH.Application.Common.Vacancy.Commands;

/// <summary>
/// Загрузка вакансий в базу данных
/// </summary>
public sealed class LoadVacanciesCommand
{
    /// <summary>
    /// Команда
    /// </summary>
    public sealed class Command : IRequest<Unit>
    {
        /// <summary>
        /// Модель аутентификации приложения
        /// </summary>
        public AuthApplicationModel AuthApplicationModel { get; set; }

        public Command(AuthApplicationModel authApplicationModel)
        {
            AuthApplicationModel = authApplicationModel;
        }
    }

    /// <summary>
    /// Обработчик
    /// </summary>
    public sealed class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IHeadHunterClient _headHunterClient; // scoped
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ILogger _logger;

        public Handler(
            IHeadHunterClient headHunterClient,
            IVacancyRepository vacancyRepository,
            ILogger<Handler> logger)
        {
            _headHunterClient = headHunterClient;
            _vacancyRepository = vacancyRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var tokenAuthorization = await 
                _headHunterClient
                .AuthorizeAsync(request.AuthApplicationModel, cancellationToken);

            if (string.IsNullOrWhiteSpace(tokenAuthorization))
                throw new Exception("Ошибка получения токена авторизации приложения");


            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 10,
            };
            IEnumerable<int> allVacanciesId = Enumerable.Range(0, 1370416); // Get max vacancies current count
            await Parallel.ForEachAsync(allVacanciesId, options, async (vacancyId, token) =>
            {
                var vacancy = await _headHunterClient.GetVacancyAsync(vacancyId, tokenAuthorization, token);

                if (string.IsNullOrWhiteSpace(vacancy)) return;

                await _vacancyRepository.InsertOneAsync(vacancy);

                _logger.LogInformation("{VacancyId}" ,vacancyId);
            });

            return Unit.Value;
        }
    }
}
