
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class i : AnimatedLetter
{
    public i() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 38.36,78.18
                    C 37.09,93.45 34.18,103.45 25.09,132.91
                      16.00,162.36 18.00,159.45 18.00,159.45",
                PathLength: 150),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 45.82,36.00
                    C 45.82,36.00 45.82,37.45 48.00,45.64",
                PathLength: 50)

             ],
        letter: "i")
    {
    }

    protected override int ViewBoxWidth => 70;
}
