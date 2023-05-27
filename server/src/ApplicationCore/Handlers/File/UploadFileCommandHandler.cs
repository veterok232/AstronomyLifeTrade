using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Files;
using AutoMapper;
using MediatR;

namespace ApplicationCore.Handlers.File;

internal class UploadFileCommandHandler : AsyncRequestHandler<UploadFileCommand>
{
    private readonly IFileUploader _fileUploader;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UploadFileCommandHandler(
        IFileUploader fileUploader,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _fileUploader = fileUploader;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    protected override async Task Handle(UploadFileCommand command, CancellationToken cancellationToken)
    {
        var file = await _fileUploader.Upload(command.File, AttachmentType.ProductImage, false);
        await _unitOfWork.Commit();
    }
}