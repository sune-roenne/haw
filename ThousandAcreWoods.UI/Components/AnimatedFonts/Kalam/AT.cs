
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class AT : AnimatedLetter
{
    public AT() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 77.50,62.25
                    C 55.50,66.50 19.75,91.00 11.25,112.25
                      2.75,133.50 3.75,147.75 10.00,152.50
                      16.25,157.25 32.75,144.00 35.50,137.50
                      38.25,131.00 58.00,111.50 64.00,69.50
                      52.50,131.50 66.25,138.00 69.25,145.00
                      72.25,152.00 101.75,178.00 123.00,157.50
                      138.25,140.50 137.00,138.00 139.50,124.75
                      142.00,111.50 143.75,75.25 120.50,51.75
                      97.25,28.25 46.25,25.25 10.25,20.50",
                PathLength: 700)

             ],
        letter: "AT")
    {
    }
    protected override int ViewBoxWidth => 150;
}
