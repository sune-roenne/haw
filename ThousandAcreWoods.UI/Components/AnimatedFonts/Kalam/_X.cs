
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _X : AnimatedLetter
{
    public _X() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 29.33,27.00
                       C 29.33,27.00 25.67,61.67 102.00,163.00",
                PathLength: 180),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 118.00,32.67
                       C 97.18,57.33 37.47,119.27 7.00,163.00",
                PathLength: 180)

             ],
        letter: "X")
    {
    }

    protected override int ViewBoxWidth => 120;
}

