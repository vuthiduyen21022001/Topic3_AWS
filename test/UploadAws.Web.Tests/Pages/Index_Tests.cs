using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace UploadAws.Pages;

public class Index_Tests : UploadAwsWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
