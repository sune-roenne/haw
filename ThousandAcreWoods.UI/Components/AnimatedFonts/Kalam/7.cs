
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class SEVEN : AnimatedLetter
{
    public SEVEN() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 9.27,40.00
                    C 24.00,41.09 50.91,30.55 93.45,30.18
                      79.09,69.27 79.45,48.55 63.82,159.82
                      58.91,154.18 60.73,154.55 54.55,146.36",
                PathLength: 355),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 41.64,99.45
                    C 65.27,96.00 79.45,96.55 99.27,89.09",
                PathLength: 100)

             ],
        letter: "7")
    {
    }

    protected override int ViewBoxWidth => 120;
}
