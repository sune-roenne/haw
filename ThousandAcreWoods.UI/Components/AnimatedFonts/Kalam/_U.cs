
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _U : AnimatedLetter
{
    public _U() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 34.12,26.25
                       C 36.88,44.62 13.88,134.88 30.75,151.25
                         47.62,167.62 71.38,147.62 83.62,131.62
                         95.88,115.62 105.12,70.62 94.50,26.62",
                PathLength: 350)
             ],
        letter: "U")
    {
    }

    protected override int ViewBoxWidth => 120;
}

