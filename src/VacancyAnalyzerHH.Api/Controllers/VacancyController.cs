using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacancyAnalyzerHH.Application.Common.Vacancy.Commands;
using VacancyAnalyzerHH.Core.Models;

namespace VacancyAnalyzerHH.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VacancyController : BaseController
{
    public VacancyController(IMediator mediator)
        : base(mediator)
    {
    }

    /// <summary>
    /// Загрузка всех вакансий в базу данных
    /// </summary>
    /// <param name="authApplicationModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("loadVacancies")]
    public async Task<IActionResult> LoadVacancies(
        AuthApplicationModel authApplicationModel,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new LoadVacanciesCommand.Command(authApplicationModel), cancellationToken);
        return Ok();
    }
}
