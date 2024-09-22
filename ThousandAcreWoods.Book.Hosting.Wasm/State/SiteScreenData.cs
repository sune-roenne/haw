using System.Text.RegularExpressions;

namespace ThousandAcreWoods.Book.Hosting.Wasm.State;

public record SiteScreenData(
    int Width,
    int Height,
    string UserAgent
    )
{
    private static readonly Regex DeviceRegex = new Regex("(android)|(webos)|(iphone)|(ipad)|(ipod)|(blackberry)|(iemobile)|(opera mini)|(mobile)", RegexOptions.IgnoreCase);
    private const int SmallWidthBreakpoint = 600;
    private const int MediumWidthBreakpoint = 950;
    private const int SmallHeightBreakpoint = 400;
    private const int MediumHeightBreakpoint = 800;

    public readonly bool IsDevice = DeviceRegex.Matches(UserAgent).Any();

    public readonly bool IsSmallWidthScreen = Width <= SmallWidthBreakpoint;

    public readonly bool IsMediumWidthScreen = Width > SmallWidthBreakpoint && Width <= MediumWidthBreakpoint;

    public readonly bool IsLargeWidthScreen = Width > MediumWidthBreakpoint;

    public readonly bool IsSmallHeightScreen = Height<= SmallHeightBreakpoint;

    public readonly bool IsMediumHeightScreen = Height > SmallHeightBreakpoint && Height <= MediumHeightBreakpoint;

    public readonly bool IsLargeHeightScreen = Height > MediumHeightBreakpoint;

}


