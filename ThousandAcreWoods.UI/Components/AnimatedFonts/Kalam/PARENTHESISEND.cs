
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class PARENTHESISEND : AnimatedLetter
{
    public PARENTHESISEND() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 59.50,9.00
                    C 75.00,23.00 89.00,61.50 86.50,122.00
                      82.50,168.50 49.50,185.50 46.50,182.50",
                PathLength: 200)

             ],
        letter: "PARENTHESISEND")
    {
    }
    protected override int ViewBoxWidth => 120;
}
