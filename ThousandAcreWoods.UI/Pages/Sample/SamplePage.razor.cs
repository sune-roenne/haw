using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ThousandAcreWoods.UI.Pages.Sample;

public partial class SamplePage
{
    [Inject]
    public AuthenticationState AuthState { get; set; }


    protected override Task OnInitializedAsync()
    {
        var user = AuthState.User;
        return base.OnInitializedAsync();   
    }

    private string SampleText = """
        'FX forward') or (PortfolioID eq '58020' and InstrumentType eq 'FX forward') or (PortfolioID eq '10901' and InstrumentType eq 'Securities lending') or (PortfolioID eq '19003' and InstrumentType eq 'Alternative investment') (SourceContext="Scd.FundAccounting.Api.Infrastructure.LiveValuation.LiveValuationInfrastructureService",ActionId="cf2b6a67-aede-4ff9-ab4a-5eacbd73bf90",ActionName="Scd.FundAccounting.Api.Controllers.LiveValuationController.EvaluateLiveValuations (Scd.FundAccounting.Api)",X-Log-Token="5f692995-fb23-468f-a207-d426bb08f5ed",RequestId="80000010-0000-ff00-b63f-84710c7967bb",RequestPath="/live-valuation/evaluate-live-valuations")
        """;

}
