using AutoMapper;
using UploadAws.Documents;
using UploadAws.Sales;

namespace UploadAws.Web;

public class UploadAwsWebAutoMapperProfile : Profile
{
    public UploadAwsWebAutoMapperProfile()
    {
        CreateMap<SaleDto, CreateUpdateSaleDto>();
        CreateMap<Document, DocumentDto>().ReverseMap();

        //Define your AutoMapper configuration here for the Web project.
    }
}
