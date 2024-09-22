
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class f : AnimatedLetter
{
    public f() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 100.50,42.38
                    C 99.75,30.38 90.75,20.00 84.88,19.88
                      79.00,19.75 67.00,29.00 61.38,52.12
                      56.38,71.12 40.88,129.50 34.62,160.50",
                PathLength: 195),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 25.38,90.75
                    C 25.38,90.75 60.62,82.00 89.62,79.62",
                PathLength: 70)
             ],
        letter: "f")
    {
    }

    protected override int ViewBoxWidth => 120;
}
