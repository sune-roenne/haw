
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class t : AnimatedLetter
{
    public t() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 64.00,46.50
                    C 67.00,66.25 44.25,132.75 41.25,151.50
                      38.25,170.25 76.25,164.50 84.00,150.00",
                PathLength: 195),
            new AnimatedLetterPath(
               PathSpecification: @"
                    M 36.25,86.75
                    C 49.25,81.75 80.75,75.50 97.50,76.75",
                PathLength: 90)
 
             ],
        letter: "t")
    {
    }
    protected override int ViewBoxWidth => 120;
}
