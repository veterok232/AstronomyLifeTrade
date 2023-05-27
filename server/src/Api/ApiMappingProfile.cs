using ApplicationCore.Models.Files;
using AutoMapper;

namespace Api;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<IFormFile, ReadableFileModel>()
            .ForMember(d => d.MimeType, o => o.MapFrom(ff => ff.ContentType))

            // IFormFile 'OpenReadStream' is Method, AgreementFile 'OpenReadStream' is Func, so we can't map it using standard map
            .ForMember(d => d.OpenReadStream, o => o.Ignore())
            .AfterMap((ff, af) => af.OpenReadStream = ff.OpenReadStream);
    }
}