
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class DOLLARSIGN : AnimatedLetter
{
    public DOLLARSIGN() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 75.50,37.75
                    C 70.75,29.75 3.25,65.00 5.75,82.00
                      7.50,103.50 70.25,110.00 73.75,122.75
                      76.50,132.75 71.00,140.75 58.25,147.75
                      43.50,156.75 21.00,158.25 10.25,152.00
                      12.25,144.00 17.50,143.75 17.50,143.75",
                PathLength: 500),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 44.50,6.00
                    C 37.75,42.00 39.25,142.00 41.25,178.25",
                PathLength: 180)

             ],
        letter: "DOLLARSIGN")
    {
    }

    protected override int ViewBoxWidth => 80;
}
