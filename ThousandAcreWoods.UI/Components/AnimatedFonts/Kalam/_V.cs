
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _V : AnimatedLetter
{
    public _V() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 15.88,25.75
                       C 14.31,33.47 32.26,138.36 47.26,156.86
                         52.38,159.85 96.16,38.03 111.64,26.99",
                PathLength: 400)
             ],
        letter: "V")
    {
    }

    protected override int ViewBoxWidth => 120;
}

