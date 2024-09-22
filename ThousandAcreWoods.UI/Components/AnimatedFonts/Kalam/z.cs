
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class z : AnimatedLetter
{
    public z() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 34.36,80.18
                    C 51.82,75.09 80.55,74.18 87.64,85.09
                      94.73,96.00 33.82,128.00 27.45,147.64
                      21.09,167.27 79.27,160.18 93.45,153.82",
                PathLength: 255)
             ],
        letter: "z")
    {
    }
    protected override int ViewBoxWidth => 120;
}
