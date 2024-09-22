
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class MINUS : AnimatedLetter
{
    public MINUS() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 38.00,116.00
                    C 53.00,117.00 79.00,114.50 84.00,112.50
                      89.00,110.50 79.00,106.00 79.00,106.00",
                PathLength: 90)

             ],
        letter: "MINUS")
    {
    }

    protected override int ViewBoxWidth => 120;
}
