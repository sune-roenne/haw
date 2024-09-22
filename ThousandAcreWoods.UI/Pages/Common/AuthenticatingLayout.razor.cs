using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.UI.Security;

namespace ThousandAcreWoods.UI.Pages.Common;

public partial class AuthenticatingLayout
{
    [CascadingParameter]
    public Task<AuthenticationState> AuthenticationState { get; set; }

    [Inject]
    public NavigationManager Navigation { get; set; }


    protected async override Task OnInitializedAsync()
    {
        var authState = await AuthenticationState;
        if (authState.User.HasClaim(_ => _.Type == AuthorizationPolicies.MemberPolicy && _.Value == true.ToString()))
        {
            await InvokeAsync(StateHasChanged);
        }
        else
        {
            Navigation.NavigateTo("entrance", forceLoad: true);
        }

    }


}
