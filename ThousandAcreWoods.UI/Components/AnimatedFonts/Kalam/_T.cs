
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _T : AnimatedLetter
{
    public _T() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 67.25,40.38
                       C 71.22,51.05 50.66,143.22 42.50,167.00",
                PathLength: 140),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 18.38,41.00
                       C 18.38,41.00 107.88,36.62 115.62,41.38",
                PathLength: 100)
             ],
        letter: "T")
    {
    }

    protected override int ViewBoxWidth => 120;
}

