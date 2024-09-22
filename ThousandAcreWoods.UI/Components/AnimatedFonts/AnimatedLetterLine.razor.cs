using Microsoft.AspNetCore.Components;

namespace ThousandAcreWoods.UI.Components.AnimatedFonts;

public partial class AnimatedLetterLine : ComponentBase
{

    [Parameter]
    public RenderFragment Characters { get; set; }


}
