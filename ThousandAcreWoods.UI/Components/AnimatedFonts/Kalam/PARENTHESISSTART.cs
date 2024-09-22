
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class PARENTHESISSTART: AnimatedLetter
{
    public PARENTHESISSTART() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 63.50,8.00
                    C 30.50,6.00 21.00,106.50 21.50,139.00
                      22.00,171.50 51.50,181.00 62.00,181.50",
                PathLength: 250)

             ],
        letter: "PARENTHESISSTART")
    {
    }

    protected override int ViewBoxWidth => 120;
}
