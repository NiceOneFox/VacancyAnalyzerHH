using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacancyAnalyzerHH.Api.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
