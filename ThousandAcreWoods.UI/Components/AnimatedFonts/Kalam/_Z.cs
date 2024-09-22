
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _Z : AnimatedLetter
{
    public _Z() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 19.25,41.38
                       C 19.25,41.38 83.75,27.00 97.62,53.75
                         103.65,67.11 23.75,119.50 16.12,151.25
                         11.50,168.25 64.25,154.00 94.88,147.62",
                PathLength: 320)

             ],
        letter: "Z")
    {
    }

    protected override int ViewBoxWidth => 120;
}

