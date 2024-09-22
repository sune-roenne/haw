
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _Y : AnimatedLetter
{
    public _Y() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 14.88,27.75
                       C 14.88,27.75 18.00,100.25 61.88,104.88",
                PathLength: 120),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 107.00,28.50
                       C 107.00,28.50 38.00,145.25 30.00,166.88",
                PathLength: 170)

             ],
        letter: "Y")
    {
    }

    protected override int ViewBoxWidth => 120;
    protected override int ViewBoxHeight => 230;

}

