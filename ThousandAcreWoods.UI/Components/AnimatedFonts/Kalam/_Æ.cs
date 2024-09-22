
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _Æ : AnimatedLetter
{
    public _Æ() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 12.25,164.25
                       C 12.25,164.25 51.50,43.25 64.00,37.50
                         76.50,31.75 130.50,32.75 163.50,34.75",
                PathLength: 250),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 103.31,32.91
                       C 103.31,32.91 106.00,40.73 102.73,58.55
                         99.09,78.91 74.00,134.91 78.73,149.09
                         91.54,177.67 145.34,140.57 155.82,142.55",
                PathLength: 320),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 33.73,105.45
                       C 33.73,105.45 113.50,92.50 147.12,92.00",
                PathLength: 120)

             ],
        letter: "Æ")
    {
    }

    protected override int ViewBoxWidth => 170;
}

