using System;
using UploadAws.Sales;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace UploadAws.Sales;

public class SaleAppService :
    CrudAppService<
       Sale, //The Book entity
        SaleDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSaleDto>, //Used to create/update a book
    ISaleAppService //implement the IBookAppService
{
    public SaleAppService(IRepository<Sale, Guid> repository)
        : base(repository)
    {

    }
}
