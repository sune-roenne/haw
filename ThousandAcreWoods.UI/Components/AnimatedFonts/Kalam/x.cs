
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class x : AnimatedLetter
{
    public x() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 38.64,70.45
                    C 40.00,84.64 75.36,147.82 91.64,164.55",
                PathLength: 150),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 101.55,73.73
                    C 96.36,87.09 32.55,145.73 25.64,160.00",
                PathLength: 150)

             ],
        letter: "x")
    {
    }
    protected override int ViewBoxWidth => 120;
}
