
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class LT : AnimatedLetter
{
    public LT() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 95.00,89.75
                    C 62.50,100.75 32.75,122.50 18.25,135.50
                      27.50,140.00 39.00,148.75 89.00,162.50",
                PathLength: 200)

             ],
        letter: "LT")
    {
    }

    protected override int ViewBoxWidth => 120;
}
