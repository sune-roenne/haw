
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class EQUALS : AnimatedLetter
{
    public EQUALS() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 13.00,85.50
                    C 38.00,88.00 82.50,88.00 89.00,82.50
                      95.50,77.00 88.50,76.00 88.50,76.00",
                PathLength: 120),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 19.00,122.50
                    C 48.00,124.00 85.50,117.00 94.00,117.50",
                PathLength: 120)


             ],
        letter: "EQUALS")
    {
    }

    protected override int ViewBoxWidth => 120;
}
