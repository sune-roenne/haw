
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class v : AnimatedLetter
{
    public v() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 35.18,75.73
                    C 35.82,89.91 49.18,149.27 54.73,161.27
                      72.00,142.45 93.91,78.91 96.09,73.45",
                PathLength: 195)
             ],
        letter: "v")
    {
    }
    protected override int ViewBoxWidth => 120;
}
