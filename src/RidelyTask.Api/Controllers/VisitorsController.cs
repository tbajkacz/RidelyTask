using MediatR;
using Microsoft.AspNetCore.Mvc;
using RidelyTask.Api.Features.GetTotalProcessedFiles;
using RidelyTask.Api.Features.GetVisitorsByCountry;
using RidelyTask.Api.Features.UploadVisitorsFile;

namespace RidelyTask.Api.Controllers;

[ApiController]
[Route("api/v1/visitors")]
public class VisitorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public VisitorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Przyjmuje plik typu json i zapisuje go w tymczasowym folderze na serwerze
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UploadVisitorsFileAsync(IFormFile file, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(
            new UploadVisitorsFileRequestDto
            {
                FileContent = file.OpenReadStream(),
                FileExtension = Path.GetExtension(file.FileName)
            }, cancellationToken));

    /// <summary>
    ///     Umożliwia pobieranie ilość wizytacji pogrupowane wg kraju
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetVisitorsByCountryAsync(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetVisitorsByCountryRequestDto(), cancellationToken));

    /// <summary>
    ///     Zwraca całkowitą liczbę przeprocesowanych plików do tej pory
    /// </summary>
    /// <returns></returns>
    [HttpGet("processed")]
    public async Task<IActionResult> GetTotalProcessedFilesAsync(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetTotalProcessedFilesRequestDto(), cancellationToken));
}