
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class BAR : AnimatedLetter
{
    public BAR() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 42.00,8.67
                    C 44.67,32.67 38.67,158.00 41.00,181.67",
                PathLength: 200)

             ],
        letter: "BAR"){}

    protected override int ViewBoxWidth => 80;
}
