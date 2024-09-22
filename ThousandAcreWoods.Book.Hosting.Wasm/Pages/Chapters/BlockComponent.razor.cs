using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.State;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class BlockComponent
{
    [Parameter]
    public DisplayParagraphType ParagraphType { get; set; }

    [Parameter]
    public DisplayBlock Block { get; set; }

    [Parameter]
    public FontSettings CenterFont { get; set; }

    [Parameter]
    public bool ShowRightSide { get; set; }

    [Parameter]
    public TimeSpan DelayPerCharacter { get; set; } = TimeSpan.Zero;

    [CascadingParameter(Name = nameof(DefaultFontSettings))]
    public FontSettings DefaultFontSettings { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }

    [Parameter]
    public bool Animate { get; set; }


}
