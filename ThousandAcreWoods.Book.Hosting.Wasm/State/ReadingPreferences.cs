using ThousandAcreWoods.Book.Hosting.Wasm.Configuration;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;
using ThousandAcreWoods.UI.Components.Common;

namespace ThousandAcreWoods.Book.Hosting.Wasm.State;

public class ReadingPreferences
{
    public ReadingPreferences(Action onPreferenceUpdate)
    {
        _onPreferenceUpdate = onPreferenceUpdate;
    }


    private FontSettings _preferredFont = new FontSettings(
        Family: SiteRenderingConstants.FontOldStandardTT, 
        Size: SiteRenderingConstants.DefaultFontSize, 
        Color: new RgbColorSpecification("rgb(0,0,0)"));
    private Action _onPreferenceUpdate;

    public FontSettings PreferredFont { get => _preferredFont; set => WrapUpdate(() => _preferredFont = value); }



    private void WrapUpdate(Action toWrap)
    {
        toWrap();
        _onPreferenceUpdate();
    }


}
