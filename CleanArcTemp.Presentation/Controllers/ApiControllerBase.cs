using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CleanArcTemp.Presentation.Controllers;


[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}



