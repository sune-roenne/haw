
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class SLASH : AnimatedLetter
{
    public SLASH() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 82.00,11.50
                    C 73.50,33.00 48.00,122.50 37.00,173.50",
                PathLength: 700)

             ],
        letter: "SLASH")
    {
    }
    protected override int ViewBoxWidth => 120;
}
