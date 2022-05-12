using MediatR;

namespace RidelyTask.Api.Features.UploadVisitorsFile;

public class UploadVisitorsFileRequestDto : IRequest
{
    public Stream FileContent { get; set; }

    public string FileExtension { get; set; }
}