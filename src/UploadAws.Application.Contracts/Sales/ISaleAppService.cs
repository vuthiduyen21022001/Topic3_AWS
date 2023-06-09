using System;

using UploadAws.Sales;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace UploadAws.Sales;

public interface ISaleAppService :
    ICrudAppService< //Defines CRUD methods
        SaleDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateSaleDto> //Used to create/update a book
{

}
