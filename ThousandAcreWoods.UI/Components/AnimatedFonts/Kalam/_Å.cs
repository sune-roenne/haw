
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _Å : AnimatedLetter
{
    public _Å() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 16.00,196.75
                       C 18.50,179.50 62.50,64.75 72.00,65.00
                         81.50,65.25 91.00,173.75 90.25,195.25",
                PathLength: 420),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 11.50,154.50
                       C 11.50,154.50 44.50,137.75 109.75,128.50",
                PathLength: 120),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 82.00,14.50
                       C 82.00,14.50 72.75,13.00 68.75,18.25
                         64.75,23.50 63.25,36.25 71.75,40.25
                         80.25,44.25 87.75,39.75 89.75,34.50
                         91.75,29.25 92.44,23.84 90.45,19.88
                         89.45,17.87 86.25,15.59 85.07,15.07
                         84.51,14.90 83.90,14.59 82.12,14.50",
                PathLength: 100)

             ],
        letter: "Å")
    {
    }

    protected override int ViewBoxWidth => 120;
    protected override int ViewBoxHeight => 230;

}

