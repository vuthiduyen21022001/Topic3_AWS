using System;
using System.Threading.Tasks;
using UploadAws.Sales;
using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace UploadAws.Web.Pages.Sales;

public class EditModalModel : UploadAwsPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public CreateUpdateSaleDto Sale { get; set; }

    private readonly ISaleAppService _saleAppService;

    public EditModalModel(ISaleAppService saleAppService)
    {
        _saleAppService = saleAppService;
    }

    public async Task OnGetAsync()
    {
        var saleDto = await _saleAppService.GetAsync(Id);
        Sale = ObjectMapper.Map<SaleDto, CreateUpdateSaleDto>(saleDto);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _saleAppService.UpdateAsync(Id, Sale);
        return NoContent();
    }
}
