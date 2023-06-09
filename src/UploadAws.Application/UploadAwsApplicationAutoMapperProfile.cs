using AutoMapper;

using UploadAws.Documents;
using UploadAws.Sales;

namespace UploadAws;

public class UploadAwsApplicationAutoMapperProfile : Profile
{
    public UploadAwsApplicationAutoMapperProfile()
    {
        CreateMap<Document, DocumentDto>().ReverseMap();

        CreateMap<Sale, SaleDto>();
        CreateMap<CreateUpdateSaleDto, Sale>();

        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
