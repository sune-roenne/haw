
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class o : AnimatedLetter
{
    public o() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 80.00,76.00
                    C 63.09,74.91 46.73,96.91 42.18,106.55
                      37.64,116.18 35.27,118.91 35.64,140.18
                      36.00,161.45 46.26,164.35 48.91,164.36
                      72.70,162.70 85.78,143.26 89.82,131.64
                      94.17,124.00 96.52,111.04 96.43,104.96
                      96.35,101.13 90.65,79.13 80.17,75.91",
                PathLength: 295)
             ],
        letter: "o")
    {
    }

    protected override int ViewBoxWidth => 120;
}
