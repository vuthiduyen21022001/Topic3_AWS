using System.Threading.Tasks;
using UploadAws.Sales;
using Microsoft.AspNetCore.Mvc;

namespace UploadAws.Web.Pages.Sales
{
    public class CreateModalModel : UploadAwsPageModel
    {
        [BindProperty]
        public CreateUpdateSaleDto Sale { get; set; }

        private readonly ISaleAppService _saleAppService;

        public CreateModalModel(ISaleAppService saleAppService)
        {
            _saleAppService = saleAppService;
        }

        public void OnGet()
        {
            Sale = new CreateUpdateSaleDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _saleAppService.CreateAsync(Sale);
            return NoContent();
        }
    }
}
