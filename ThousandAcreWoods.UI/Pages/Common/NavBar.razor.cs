using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using ThousandAcreWoods.UI.Configuration;

namespace ThousandAcreWoods.UI.Pages.Common;

public partial class NavBar
{


    [Inject]
    public IOptions<ServiceConfiguration> Config { get; set; }


    protected async override Task OnParametersSetAsync()
    {
    }

}
